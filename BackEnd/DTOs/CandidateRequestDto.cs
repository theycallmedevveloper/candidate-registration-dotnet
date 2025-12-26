namespace CandidateRegistration.DTOs
{
    public class CandidateRequestDto
    {

        public int? Candidate_Id { get; set; }
        public int? Prefix_Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public int? Gender_Id { get; set; }
        public string? Dob { get; set; }
        public int? MaritalStatus_Id { get; set; }

        public string? Candidate_Email { get; set; }
        public string? Candidate_Num { get; set; }
    }
}