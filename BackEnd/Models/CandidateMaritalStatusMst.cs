using System.ComponentModel.DataAnnotations;

namespace CandidateRegistration.Models
{
    public class CandidateMaritalStatusMst
    {
        [Key]
        public int MaritalStatus_Id { get; set; }
        public string? MaritalStatus_Name { get; set; }
        public bool? IsActive { get; set; }
    }
}