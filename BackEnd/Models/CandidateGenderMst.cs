using System.ComponentModel.DataAnnotations;

namespace CandidateRegistration.Models
{
    public class CandidateGenderMst
    {
        [Key]
        public int Gender_Id { get; set; }

        public string? Gender_Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
