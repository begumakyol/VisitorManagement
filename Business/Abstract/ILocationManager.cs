using VisitorManagementSystem.Models.Entities;

namespace VisitorManagementSystem.Business.Abstract
{
    public interface ILocationManager
    {
        public IList<Location> GetLocations();
        public void AddLocation(Location location);
    }
}
