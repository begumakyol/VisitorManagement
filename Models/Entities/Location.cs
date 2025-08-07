namespace VisitorManagementSystem.Models.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public List<Visitor>? Visitors { get; set; }
    }
}