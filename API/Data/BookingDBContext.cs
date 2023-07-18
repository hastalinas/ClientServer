using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class BookingDBContext : DbContext
{
    public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options) { }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    // for foreign key
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
                    .HasIndex(e => new{
                        e.Nik,
                        e.Email,
                        e.PhoneNumber
                    }).IsUnique();

        // Many education with one university (N:1)
        modelBuilder.Entity<Education>().HasOne(e => e.University).WithMany(u => u.Educations).HasForeignKey(u => u.UniversityGuid);
        modelBuilder.Entity<Education>().HasOne(e => e.Employee).WithOne(em => em.Education).HasForeignKey<Employee>(em => em.Guid);

        // Many booking with one room
        modelBuilder.Entity<Booking>().HasOne(r => r.Room).WithMany(b => b.Bookings).HasForeignKey(b => b.RoomGuid);

        // booking with employee
        modelBuilder.Entity<Booking>().HasOne(e => e.Employee).WithMany(b => b.Bookings).HasForeignKey(b => b.EmployeeGuid);

        // account with employee
        modelBuilder.Entity<Account>().HasOne(e => e.Employee).WithOne(a => a.Account).HasForeignKey<Employee>(a => a.Guid);

        // account with account role
        modelBuilder.Entity<Account>().HasMany(ac => ac.AccountRoles).WithOne(a => a.Account).HasForeignKey(a => a.AccountGuid);

        // account role with role
        modelBuilder.Entity<AccountRole>().HasOne(r => r.Role).WithMany(a => a.AccountRoles).HasForeignKey(a => a.RoleGuid);
    }
}

