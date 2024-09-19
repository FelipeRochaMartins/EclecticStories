using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class EStoriesContext : IdentityDbContext<IdentityUser>
    {
        public EStoriesContext(DbContextOptions<EStoriesContext> options) : base(options)
        {

        }

        public DbSet<BookModel> Book { get; set; }
        public DbSet<PageModel> Page { get; set; }
        public DbSet<ComentaryModel> Comentary { get; set; }
        public DbSet<HistoryModel> History { get; set; }
        public DbSet<BookCoverModel> BookCover { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComentaryModel>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Commentaries)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComentaryModel>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PageModel>()
                .HasOne(p => p.Book)
                .WithMany(b => b.Pages)
                .HasForeignKey(p => p.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Publisher)
                .WithMany()
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoryModel>()
                .HasOne(h => h.User)
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoryModel>()
                .HasOne(h => h.Book)
                .WithMany()
                .HasForeignKey(h => h.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.BookCover)    
                .WithOne(bc => bc.Book)
                .HasForeignKey<BookCoverModel>(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
