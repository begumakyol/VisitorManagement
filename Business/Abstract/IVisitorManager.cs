using VisitorManagementSystem.Models.Entities;
using VisitorManagementSystem.Models.ViewModels;

namespace VisitorManagementSystem.Business.Abstract
{
    public interface IVisitorManager
    {
        public void AddVisitor(Visitor visitor, string locationIdStr, string recordedBy);
        public void UpdateVisitor(Visitor visitor);
        public List<Visitor> ListVisitor(DateTime? startDate, DateTime? endDate, int locationId);
        public List<VisitorViewModel> ListVisitorViewModel(DateTime? startDate, DateTime? endDate, int locationId);
        public Visitor GetVisitorById(long id);
        public void PrepareVisitorForAdd(Visitor visitor, string locationIdStr, string recordedBy);
        public void EncryptVisitorFields(Visitor visitor);
        public void DecryptVisitorFields(Visitor visitor);
        public bool VisitorExists(long visitorId);
        public void MarkVisitorAsExited(int visitorId, string stoppedBy);
        public Dictionary<string, int> GetWeeklyVisitors();
        public int GetTodayVisitorCount();
        public int GetNotExitedVisitorCount();
    }
}
