using CandidateRegistration.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandidateRegistration.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GenderController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetGenders()
        {
            var genders = _context.CandidateGenderMst.ToList();
            return Ok(genders);
        }

        [HttpPost]
        public IActionResult AddGender(CandidateGenderMst gender)
        {
            _context.CandidateGenderMst.Add(gender);
            _context.SaveChanges();
            return Ok(gender);
        }
    }

}