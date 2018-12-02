namespace smileRed.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<smileRed.Domain.User> Users { get; set; }

        public DbSet<smileRed.Domain.TypeofUser> TypeofUsers { get; set; }

        public DbSet<smileRed.Domain.Product> Products { get; set; }

        public DbSet<smileRed.Domain.Group> Groups { get; set; }

        public DbSet<smileRed.Domain.Admixtures> Admixtures { get; set; }

        public DbSet<smileRed.Backend.Controllers.Nutrition> Nutritions { get; set; }

        public DbSet<smileRed.Domain.Offert> Offerts { get; set; }

        public DbSet<smileRed.Domain.Order> Orders { get; set; }

        public DbSet<smileRed.Domain.OrderStatus> OrderStatus { get; set; }

        public DbSet<smileRed.Domain.OrderDetails> OrderDetails { get; set; }

        public DbSet<smileRed.Domain.Contacts> Contacts { get; set; }

        public DbSet<smileRed.Domain.Favorite> Favorites { get; set; }

        public DbSet<smileRed.Domain.Reservation> Reservations { get; set; }
    }
}