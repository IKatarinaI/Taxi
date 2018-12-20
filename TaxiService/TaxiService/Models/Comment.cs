using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiService.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string Date { get; set; } 

        public int UserId { get; set; }

        public int RideId { get; set; }

        public string Grade { get; set; }      
    }
}