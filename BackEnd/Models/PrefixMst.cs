using System.ComponentModel.DataAnnotations;

namespace CandidateRegistration.Models
{
    public class PrefixMst
    {
        [Key]
        public int Prefix_Id { get; set; }

        public string? Prefix_Name { get; set; }
        public bool? IsActive { get; set; }
    }

}