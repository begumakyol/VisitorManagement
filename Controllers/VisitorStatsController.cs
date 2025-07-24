using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Business.Abstract;

namespace VisitorManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorStatsController : Controller
    {
        private readonly IVisitorManager _visitorManager;

        public VisitorStatsController(IVisitorManager visitorManager)
        {
            _visitorManager = visitorManager;
        }

        [HttpGet("weekly")]
        public IActionResult GetWeeklyVisitors()
        {
            var result = _visitorManager.GetWeeklyVisitors();
            return Ok(result);
        }

        [HttpGet("daily")]
        public IActionResult GetTodayStats()
        {
            var todayVisitorCount = _visitorManager.GetTodayVisitorCount();
            var notExitedVisitorCount = _visitorManager.GetNotExitedVisitorCount();
            var monthlyVisitorCount = _visitorManager.GetMonthlyVisitorCount();

            return Ok(new
            {
                todayVisitorCount,
                notExitedVisitorCount,
                monthlyVisitorCount
            });
        }
    }
}
