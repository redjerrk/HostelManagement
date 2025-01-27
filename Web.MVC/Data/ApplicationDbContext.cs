using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.MVC.Models;

namespace Web.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }
}