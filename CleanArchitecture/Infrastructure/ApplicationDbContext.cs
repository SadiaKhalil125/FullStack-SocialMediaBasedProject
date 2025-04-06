using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bonded.Domain;
namespace Bonded.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IdentityBondedProjectDB;Integrated Security=True");
        //}
        public DbSet<User> Users {  get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Convo> Convos { get; set; }    
        public DbSet<Chat> Chats {  get; set; }
        public DbSet<CarousalImage> CarousalImages {  get; set; }

    }
}
