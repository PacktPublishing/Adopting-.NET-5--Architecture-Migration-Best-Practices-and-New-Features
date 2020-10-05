using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        [Range(0, 5)]
        public int? Rating { get; set; }
        public string Review { get; set; }
        public string Title { get; set; }
    }
}
