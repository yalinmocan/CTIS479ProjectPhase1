using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal? Rating { get; set; }
        public DateTime PublishDate { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }


    }
}
