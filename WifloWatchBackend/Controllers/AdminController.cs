using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WifloWatchBackend.Data;

namespace WifloWatchBackend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly WifloWatchDbContext _context;

        public AdminController(WifloWatchDbContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            // Admin'e özel veri döndürme (istatistikler, kullanıcı sayısı vs.)
            var stats = new { UserCount = _context.Users.Count() };
            return Ok(stats);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // Admin'e özgü diğer işlemleri burada yapabilirsiniz
    }

}
