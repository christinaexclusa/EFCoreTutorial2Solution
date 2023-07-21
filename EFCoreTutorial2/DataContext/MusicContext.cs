using EFCoreTutorial2.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial2.DataContext
{
    public class MusicContext : DbContext
    {
        private const string CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=EFCoreTutor;Trusted_Connection=True;MultipleActiveResultSets=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }

        public DbSet<SongModel> Songs { get; set; }
        public DbSet<ArtistModel> Artists { get; set; }

        public DbSet<GenreModel>  Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreModel>().HasData(
                new GenreModel { Id = 1, Name = "Rock and Roll" },
                new GenreModel { Id = 2, Name = "R&B" },
                new GenreModel { Id = 3, Name = "Country" });
            modelBuilder.Entity<ArtistModel>().HasData(
                new ArtistModel { Id = 1, Name = "Johnny Cash" },
                new ArtistModel { Id = 2, Name = "Jimmy Buffet" },
                new ArtistModel { Id = 3, Name = "Home Free" });
            modelBuilder.Entity<SongModel>().HasData(
                new SongModel { Id = 1, Title = "You're Heartless", ArtistId = 1, GenreId = 1 },
                new SongModel { Id = 2, Title = "Ride Bikes", ArtistId = 1, GenreId = 1 },
                new SongModel { Id = 3, Title = "Wayfaring", ArtistId = 1, GenreId = 2 },
                new SongModel { Id = 4, Title = "Son of a Sailor", ArtistId = 2, GenreId = 3 },
                new SongModel { Id = 5, Title = "Sea Shanty", ArtistId = 3, GenreId = 3 },
                new SongModel { Id = 6, Title = "Island", ArtistId = 2, GenreId = 3 });
        }
    }
}
