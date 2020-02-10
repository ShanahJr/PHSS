using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PHSS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<TeamModel> Teams { get; set; }
        public virtual DbSet<PlayerModel> Players { get; set; }
        public virtual DbSet<DivisionModel> Divisions { get; set; }
        public virtual DbSet<SchoolModel> Schools { get; set; }
        public virtual DbSet<FixtureModel> Fixtures { get; set; }
        public virtual DbSet<LogModel> Logs { get; set; }
        public virtual DbSet<ResultModel> Results { get; set; }
        public virtual DbSet<SeasonModel> Seasons { get; set; }
        public virtual DbSet<AgeGroupModel> AgeGroup { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}