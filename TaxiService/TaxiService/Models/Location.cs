using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiService.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string X { get; set; }

        public string Y { get; set; }

        public int AddressId { get; set; }
    }
}