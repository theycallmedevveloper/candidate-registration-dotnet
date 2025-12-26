using Microsoft.AspNetCore.Mvc;
using CandidateRegistration.Models;

namespace CandidateRegistration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaritalStatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaritalStatusController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMaritalStatuses()
        {
            var statuses = _context.CandidateMaritalStatusMst
                                   .Where(x => x.IsActive == true)
                                   .ToList();

            return Ok(statuses);
        }

        [HttpPost]
        public IActionResult AddMaritalStatus(CandidateMaritalStatusMst status)
        {
            _context.CandidateMaritalStatusMst.Add(status);
            _context.SaveChanges();

            return Ok(status);
        }
    }
}
