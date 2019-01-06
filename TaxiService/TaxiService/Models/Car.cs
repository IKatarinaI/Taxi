using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiService.Models
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public int? DriverId { get; set; }

        public int Year { get; set; }

        public string Registation { get; set; }

        public string CarNumber { get; set; }

        public string TypeOfCar { get; set; }
    }
}