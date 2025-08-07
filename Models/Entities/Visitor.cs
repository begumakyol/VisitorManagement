namespace VisitorManagementSystem.Models.Entities
{
    public class Visitor
    {
        public long VisitorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string CitizenshipNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string MeetingWith { get; set; } = string.Empty;
        public string Gender { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool IsInside { get; set; } = true;
        public short TimeSpentInMinutes { get; set; }
        public string StartRecordingBy { get; set; } = string.Empty;
        public string StopRecordingBy { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}