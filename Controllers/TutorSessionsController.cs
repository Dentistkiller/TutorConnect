using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorConnect.Models;

namespace TutorConnect.Controllers
{
    public class TutorSessionsController : Controller
    {
        private readonly TutorConnectDbContext _context;

        public TutorSessionsController(TutorConnectDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _context.TutorSessions.ToListAsync();
            return View(sessions);
        }
    }
}
