using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreWebAPI_Homework.Logic.ApiModels
{
    public class BookApiModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 9999)]
        public int Year { get; set; }

        [Required]
        [MaxLength(40)]
        public string AuthorName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PublisherName { get; set; }

        public IEnumerable<string> Genres { get; set; }
    }
}
