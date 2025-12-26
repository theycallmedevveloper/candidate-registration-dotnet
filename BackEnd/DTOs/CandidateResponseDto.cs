namespace CandidateRegistration.DTOs
{
    public class CandidateResponseDto
    {
        internal bool IsActive;

        public int Candidate_Id { get; set; }
        public int? Prefix_Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int? Gender_Id { get; set; }
        public string? Dob { get; set; }
        public int? MaritalStatus_Id { get; set; }
        public string? Email { get; set; }
        public string? Number { get; set; }

        public PrefixDto? Prefix { get; set; }
        public GenderDto? Gender { get; set; }
        public MaritalStatusDto? MaritalStatus { get; set; }
    }

    public class PrefixDto
    {
        public int Prefix_Id { get; set; }
        public string? Prefix_Name { get; set; }
    }

    public class GenderDto
    {
        public int Gender_Id { get; set; }
        public string? Gender_Name { get; set; }
    }

    public class MaritalStatusDto
    {
        public int MaritalStatus_Id { get; set; }
        public string? MaritalStatus_Name { get; set; }
    }
}