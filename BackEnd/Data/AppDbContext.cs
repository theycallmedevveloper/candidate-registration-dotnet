using Microsoft.EntityFrameworkCore;
using CandidateRegistration.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<CandidateMst> CandidateMst { get; set; }
    public DbSet<CandidateGenderMst> CandidateGenderMst { get; set; }
    public DbSet<CandidateMaritalStatusMst> CandidateMaritalStatusMst { get; set; }
    public DbSet<PrefixMst> PrefixMst { get; set; }

}
