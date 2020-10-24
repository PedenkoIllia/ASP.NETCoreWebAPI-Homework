using ASP.NETCoreWebAPI_Homework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebAPI_Homework.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.GenreBooks)
                .HasForeignKey(bg => bg.GenreId);

            modelBuilder.Entity<Genre>()
                .HasData(
                    new Genre[]
                    {
                        new Genre { Id = 1, Name = "Action and adventure"},
                        new Genre { Id = 2, Name = "Drama"},
                        new Genre { Id = 3, Name = "Crime"},
                        new Genre { Id = 4, Name = "Fantasy"},
                        new Genre { Id = 5, Name = "History"},
                        new Genre { Id = 6, Name = "Humor"},
                        new Genre { Id = 7, Name = "Horror"},
                        new Genre { Id = 8, Name = "Novel"},
                        new Genre { Id = 9, Name = "Philosophy"},
                        new Genre { Id = 10, Name = "Science"},
                        new Genre { Id = 11, Name = "Science fiction"}
                    }
                );

            modelBuilder.Entity<Book>()
                .HasData(
                    new Book[]
                    { 
                        new Book { Id = 1, Name = "Harry Potter and the Philosopher's Stone", AuthorName = "J.K. Rowling", PublisherName = "Pottermore Publishing", Year = 2015 },
                        new Book { Id = 2, Name = "Dracula", AuthorName = "Bram Stoker", PublisherName = "Archibald Constable and Company", Year = 1897 },
                    }
                );

            modelBuilder.Entity<BookGenre>()
                .HasData(
                    new BookGenre[]
                    {
                        new BookGenre { BookId = 1, GenreId = 1 },
                        new BookGenre { BookId = 1, GenreId = 4 },
                        new BookGenre { BookId = 1, GenreId = 11 },
                        new BookGenre { BookId = 2, GenreId = 4 },
                        new BookGenre { BookId = 2, GenreId = 7 },
                        new BookGenre { BookId = 2, GenreId = 9 },
                    }
    );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
    }
}
