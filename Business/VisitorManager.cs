using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.Entities;
using VisitorManagementSystem.Utilities;
using VisitorManagementSystem.Models.ViewModels;


namespace VisitorManagementSystem.Business
{
    public class VisitorManager : IVisitorManager
    {
        private readonly VisitorDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VisitorManager(VisitorDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public void AddVisitor(Visitor visitor, string locationIdStr, string recordedBy)
        {
            PrepareVisitorForAdd(visitor, locationIdStr, recordedBy);

            _context.Visitors.Add(visitor);
            _context.SaveChanges();
        }

        public List<Visitor> ListVisitor(DateTime? startDate, DateTime? endDate, int locationId)
        {
            var query = _context.Visitors
                        .Where(v => v.LocationId == locationId)
                        .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(v => v.EntryDate.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(v => v.EntryDate.Date <= endDate.Value);
            }

            var visitors = query.ToList();

            visitors.ForEach(visitor =>
            {
                DecryptVisitorFields(visitor);
                visitor.IsInside = visitor.ExitDate == DateTime.MinValue;
            });

            return visitors;
        }

        public List<VisitorViewModel> ListVisitorViewModel(DateTime? startDate, DateTime? endDate, int locationId)
        {
            var query = _context.Visitors
                .Where(v => v.LocationId == locationId)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(v => v.EntryDate.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(v => v.EntryDate.Date <= endDate.Value);
            }

            var visitors = query.ToList();

            var visitorViewModels = visitors.Select(visitor =>
            {
                DecryptVisitorFields(visitor);

                return new VisitorViewModel
                {
                    VisitorId = visitor.VisitorId,
                    FullName = visitor.FullName,
                    CitizenshipNumber = visitor.CitizenshipNumber,
                    CompanyName = visitor.CompanyName,
                    MeetingWith = visitor.MeetingWith,
                    Gender = visitor.Gender,
                    EntryDate = visitor.EntryDate,
                    ExitDate = visitor.ExitDate,
                    IsInside = visitor.IsInside,
                };
            }).ToList();

            return visitorViewModels;
        }

        public void UpdateVisitor(Visitor visitor)
        {
            var existing = _context.Visitors.FirstOrDefault(v => v.VisitorId == visitor.VisitorId);
            if (existing is null) return;


            EncryptVisitorFields(visitor);

            existing.FullName = visitor.FullName;
            existing.CitizenshipNumber = visitor.CitizenshipNumber;
            existing.CompanyName = visitor.CompanyName;
            existing.MeetingWith = visitor.MeetingWith;
            existing.Gender = visitor.Gender;

            _context.Update(existing);
            _context.SaveChanges();
        }

        public void MarkVisitorAsExited(int visitorId, string stoppedBy)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.VisitorId == visitorId);
            if (visitor == null) return;

            visitor.ExitDate = DateTime.Now;
            visitor.TimeSpentInMinutes = (short)(visitor.ExitDate - visitor.EntryDate).TotalMinutes;
            visitor.IsInside = false;
            visitor.StopRecordingBy = stoppedBy;

            _context.SaveChanges();
        }

        public Visitor GetVisitorById(long id)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.VisitorId == id);
            if (visitor != null)
            {
                DecryptVisitorFields(visitor);
            }
            return visitor!;
        }

        public void PrepareVisitorForAdd(Visitor visitor, string locationIdStr, string recordedBy)
        {
            if (int.TryParse(locationIdStr, out int locationId))
            {
                visitor.LocationId = locationId;
            }

            visitor.StartRecordingBy = recordedBy;
            visitor.EntryDate = DateTime.Now;

            EncryptVisitorFields(visitor);
        }

        public void EncryptVisitorFields(Visitor visitor)
        {
            visitor.FullName = StringCipher.Encrypt(visitor.FullName);
            visitor.CitizenshipNumber = StringCipher.Encrypt(visitor.CitizenshipNumber);
        }

        public void DecryptVisitorFields(Visitor visitor)
        {
            visitor.FullName = StringCipher.Decrypt(visitor.FullName);
            visitor.CitizenshipNumber = StringCipher.Decrypt(visitor.CitizenshipNumber);
        }

        public bool VisitorExists(long visitorId)
        {
            return _context.Visitors.Any(v => v.VisitorId == visitorId);
        }

        public Dictionary<string, int> GetWeeklyVisitors()
        {
            var locationId = GetCurrentLocationId();
            if (locationId == null)
                return new Dictionary<string, int>();

            var start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1); // Pazartesi
            var end = start.AddDays(5); // Cuma

            var data = _context.Visitors
                                    .Where(v => v.LocationId == locationId.Value &&
                                    v.EntryDate >= start && v.EntryDate <= end)
                                    .ToList()
                                    .GroupBy(v => v.EntryDate.DayOfWeek)
                                    .ToDictionary(
                                    g => g.Key.ToString(),
                                    g => g.Count());

            var days = new Dictionary<string, string>
            {
                ["Monday"] = "Pazartesi",
                ["Tuesday"] = "Salı",
                ["Wednesday"] = "Çarşamba",
                ["Thursday"] = "Perşembe",
                ["Friday"] = "Cuma"
            };

            var result = days.ToDictionary(
                d => d.Value,
                d => data.ContainsKey(d.Key) ? data[d.Key] : 0
            );

            return result;
        }

        public int GetTodayVisitorCount()
        {
            var locationId = GetCurrentLocationId();
            if (locationId == null) return 0;

            var today = DateTime.Today;
            return _context.Visitors
                .Count(v => v.LocationId == locationId.Value &&
                            v.EntryDate.Date == today);
        }

        public int GetNotExitedVisitorCount()
        {
            var locationId = GetCurrentLocationId();
            if (locationId == null) return 0;

            return _context.Visitors
                .Count(v => v.LocationId == locationId.Value &&
                            v.IsInside);
        }

        private int? GetCurrentLocationId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var locationIdClaim = user?.Claims.FirstOrDefault(c => c.Type == "LocationId");
            if (locationIdClaim != null && int.TryParse(locationIdClaim.Value, out int locationId))
            {
                return locationId;
            }

            return null;
        }
    }
}