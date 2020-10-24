using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreWebAPI_Homework.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<BookGenre> GenreBooks { get; set; }

        public Genre()
        {
            GenreBooks = new List<BookGenre>();
        }
    }
}
