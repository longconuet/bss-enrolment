using Enrolment.Models;
using Microsoft.EntityFrameworkCore;

namespace Enrolment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<EmailRegister> EmailRegisters { set; get; }
        public DbSet<Payee> Payees { set; get; }
        public DbSet<Payer> Payers { set; get; }
        public DbSet<Employee> Employees { set; get; }
        public DbSet<Employer> Employers { set; get; }
        public DbSet<IdentityProofImage> IdentityProofImages { set; get; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // configures one-to-many relationship
            //builder.Entity<EmailRegister>()
            //    .HasOme(s => s.Payer)
            //    .WithOn(g => g.CourseStudents)
            //    .HasForeignKey(s => s.CourseId);

            //builder.Entity<CourseStudent>()
            //   .HasOne(s => s.Student)
            //   .WithMany(g => g.CourseStudents)
            //   .HasForeignKey(s => s.StudentId);
        }
    }
}
