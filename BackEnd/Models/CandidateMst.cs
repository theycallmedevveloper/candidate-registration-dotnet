using CandidateRegistration.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CandidateRegistration.Models
{
    public class CandidateMst
    {
        [Key]
        public int Candidate_Id { get; set; }

        public string? Candidate_FirstName { get; set; }
        public string? Candidate_MiddleName { get; set; }
        public string? Candidate_LastName { get; set; }
        public int? MaritalStatus_Id { get; set; }
        public int? Prefix_Id { get; set; }
        public int? Gender_Id { get; set; }
        public DateTime? Candidate_Dob { get; set; }
        public bool? IsActive { get; set; }

        public string? Candidate_Num { get; set; }

        public string? Candidate_Email { get; set; }

    }
}