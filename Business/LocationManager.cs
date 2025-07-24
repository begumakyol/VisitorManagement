using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Business.Abstract;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Models.Entities;

namespace VisitorManagementSystem.Business
{
    public class LocationManager : ILocationManager
    {
        private readonly VisitorDbContext _context;

        public LocationManager(VisitorDbContext context)
        {
            _context = context;
        }

        public IList<Location> GetLocations()
        {
            return _context.Set<Location>().ToList();
        }

        public void AddLocation(Location location)
        {
            _context.Add(location);
            _context.SaveChanges();
        }
    }
}
