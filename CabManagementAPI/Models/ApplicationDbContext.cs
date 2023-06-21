using Microsoft.EntityFrameworkCore;
using CabManagementAPI.Models;

    namespace CabManagementAPI.Models
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CarBookModel> carBookModels { get; set; }
        public DbSet<UserRegistrationModel> userRegistrationModels { get; set; }
        public DbSet<ContactUsModel> contactUsModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {



            builder.Entity<CarBookModel>().ToTable("CarRegistration");
             builder.Entity<UserRegistrationModel>().ToTable("UserRegistration");
            builder.Entity<ContactUsModel>().ToTable("ContactAdmin");



        }
            





    }
}
