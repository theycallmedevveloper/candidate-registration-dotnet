using CandidateRegistration.DTOs;
using CandidateRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace CandidateRegistration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CandidateController(AppDbContext context)
        {
            _context = context;
        }

        private static List<CandidateResponseDto> _candidates = new List<CandidateResponseDto>();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAllCandidates()
        {

            var prefixes = (
                from c in _context.CandidateMst
                join p in _context.PrefixMst
            on c.Prefix_Id equals p.Prefix_Id
                join g in _context.CandidateGenderMst
                    on c.Gender_Id equals g.Gender_Id
                join m in _context.CandidateMaritalStatusMst
                    on c.MaritalStatus_Id equals m.MaritalStatus_Id
                where c.IsActive == true
                select new CandidateResponseDto

                {
                    Candidate_Id = c.Candidate_Id,
                    FirstName = c.Candidate_FirstName,
                    MiddleName = c.Candidate_MiddleName,
                    LastName = c.Candidate_LastName,
                    Dob = c.Candidate_Dob.HasValue ? c.Candidate_Dob.Value.ToString("dd/MM/yyyy") : null,

                    Email = c.Candidate_Email,
                    Number = c.Candidate_Num,
                    Prefix = new PrefixDto
                    {
                        Prefix_Id = p.Prefix_Id,
                        Prefix_Name = p.Prefix_Name
                    },

                    Gender = new GenderDto
                    {
                        Gender_Id = g.Gender_Id,
                        Gender_Name = g.Gender_Name
                    },

                    MaritalStatus = new MaritalStatusDto
                    {
                        MaritalStatus_Id = m.MaritalStatus_Id,
                        MaritalStatus_Name = m.MaritalStatus_Name
                    }

                }).ToList();

            return Ok(prefixes);
        }

        [HttpGet("{id}")]
        public IActionResult GetCandidateById(int id)
        {
            var candidate = _context.CandidateMst.SingleOrDefault(x => x.Candidate_Id == id && x.IsActive == true);
            if (candidate == null)
            {
                return NotFound(new { message = "Candidate  not found" });
            }

            return Ok(candidate);
        }


        [HttpPost]
        public IActionResult SaveCandidate([FromBody] CandidateRequestDto candidateData)
        {

            var candidate = new CandidateMst
            {
                Candidate_Id = 0,
                Prefix_Id = candidateData.Prefix_Id,
                Candidate_FirstName = candidateData.FirstName,
                Candidate_MiddleName = candidateData.MiddleName,
                Candidate_LastName = candidateData.LastName,
                Gender_Id = candidateData.Gender_Id,
                MaritalStatus_Id = candidateData.MaritalStatus_Id,
                Candidate_Dob = DateOnly.ParseExact(candidateData.Dob, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Candidate_Email = candidateData.Candidate_Email,
                Candidate_Num = candidateData.Candidate_Num,
                IsActive = true
            };

            // _candidates.Add(candidate);
            _context.CandidateMst.Add(candidate);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Candidate saved successfully",
                data = candidate
            });
        }


        [HttpPost("SaveCandidate1")]
        public IActionResult SaveCandidate1([FromBody] CandidateRequestDto candidateData)
        {

            if (candidateData.Candidate_Id == 0)
            {
                var candidate = new CandidateMst
                {
                    Candidate_Id = 0,
                    Prefix_Id = candidateData.Prefix_Id,
                    Candidate_FirstName = candidateData.FirstName,
                    Candidate_MiddleName = candidateData.MiddleName,
                    Candidate_LastName = candidateData.LastName,
                    Gender_Id = candidateData.Gender_Id,
                    MaritalStatus_Id = candidateData.MaritalStatus_Id,
                    Candidate_Dob = DateOnly.ParseExact(candidateData.Dob, "dd/MM/yyyy", null),
                    Candidate_Email = candidateData.Candidate_Email,
                    Candidate_Num = candidateData.Candidate_Num,
                    IsActive = true
                };

                // _candidates.Add(candidate);
                _context.CandidateMst.Add(candidate);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Candidate saved successfully",
                    data = candidate
                });


            }
            else
            {
                var candidate = new CandidateMst
                {
                    Candidate_Id = (int)candidateData.Candidate_Id,
                    Prefix_Id = candidateData.Prefix_Id,
                    Candidate_FirstName = candidateData.FirstName,
                    Candidate_MiddleName = candidateData.MiddleName,
                    Candidate_LastName = candidateData.LastName,
                    Gender_Id = candidateData.Gender_Id,
                    MaritalStatus_Id = candidateData.MaritalStatus_Id,
                    Candidate_Dob = DateOnly.ParseExact(candidateData.Dob, "dd/MM/yyyy", null),
                    Candidate_Email = candidateData.Candidate_Email,
                    Candidate_Num = candidateData.Candidate_Num,
                    IsActive = true
                };

                // _candidates.Add(candidate);
                _context.CandidateMst.Update(candidate);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Candidate saved successfully",
                    data = candidate
                });
            }
        }


        // DELETE: api/candidate/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCandidate(int id)
        {
            var candidate = _context.CandidateMst
                .FirstOrDefault(c => c.Candidate_Id == id);

            if (candidate == null)
            {
                return NotFound(new { message = "Candidate not found" });
            }

            if ((bool)candidate.IsActive)
            {
                candidate.IsActive = false;
                _context.SaveChanges();

                return Ok(new { message = "Candidate deleted successfully" });
            }

            return BadRequest(new { message = "Candidate already deleted" });
        }
    }
}