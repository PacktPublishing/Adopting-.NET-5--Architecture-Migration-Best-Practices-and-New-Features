using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        [JsonIgnore]
        public byte[] CoverImage { get; set; }
        [Required]
        public string Author { get; set; }
        public ICollection<BookReview> Reviews { get; set; }
    }
}
