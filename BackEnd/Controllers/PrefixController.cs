using Microsoft.AspNetCore.Mvc;
using CandidateRegistration.Models;

namespace CandidateRegistration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrefixController : ControllerBase
    {   
        private readonly AppDbContext _context;

        public PrefixController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPrefixes()
        {
            var prefixes = _context.PrefixMst
                                   .Where(x => x.IsActive == true)
                                   .ToList();

            return Ok(prefixes);
        }
        
        [HttpPost]
        public IActionResult AddPrefix(PrefixMst prefix)
        {
            _context.PrefixMst.Add(prefix);
            _context.SaveChanges();

            return Ok(prefix);
        }
    }
}
