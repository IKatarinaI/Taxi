using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiService.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Geocoding { get; set; }
    }
}