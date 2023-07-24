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
        // untuk mencegah duplikasi data
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
                    .HasIndex(e => new{
                        e.Nik,
                        e.Email,
                        e.PhoneNumber
                    }).IsUnique();

        // Many education with one university (N:1)
        modelBuilder.Entity<Education>().HasOne(e => e.University)
                                        .WithMany(u => u.Educations)
                                        .HasForeignKey(u => u.UniversityGuid)
                                        .OnDelete(DeleteBehavior.Restrict);

        // employee with education
        modelBuilder.Entity<Education>().HasOne(education => education.Employee)
                                        .WithOne(employee => employee.Education)
                                        .HasForeignKey<Education>(education => education.Guid)
                                        .OnDelete(DeleteBehavior.Restrict);

        // Many booking with one room
        modelBuilder.Entity<Booking>().HasOne(r => r.Room)
                                      .WithMany(b => b.Bookings)
                                      .HasForeignKey(b => b.RoomGuid)
                                      .OnDelete(DeleteBehavior.Restrict);

        // booking with employee
        modelBuilder.Entity<Booking>().HasOne(e => e.Employee)
                                      .WithMany(b => b.Bookings)
                                      .HasForeignKey(b => b.EmployeeGuid)
                                      .OnDelete(DeleteBehavior.Restrict);

        // account with employee -> account fk
        modelBuilder.Entity<Account>().HasOne(account => account.Employee)
                                      .WithOne(employee => employee.Account)
                                      .HasForeignKey<Account>(account => account.Guid)
                                      .OnDelete(DeleteBehavior.Restrict);

        // account with account role
        modelBuilder.Entity<Account>().HasMany(ac => ac.AccountRoles)
                                      .WithOne(a => a.Account)
                                      .HasForeignKey(a => a.AccountGuid)
                                      .OnDelete(DeleteBehavior.Restrict);

        // account role with role
        modelBuilder.Entity<AccountRole>().HasOne(r => r.Role)
                                          .WithMany(a => a.AccountRoles)
                                          .HasForeignKey(a => a.RoleGuid)
                                          .OnDelete(DeleteBehavior.Restrict);
    }
}

