using Microsoft.EntityFrameworkCore;
using SBSCTechnicalAssessmentData.Models;

namespace SBSCTechnicalAssessmentData
{
    public class SBSCDataContext : DbContext
    {
        public SBSCDataContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
