using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiService.Models
{
    [Table("Rides")]
    public class Ride
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Date { get; set; } 

        [Required]
        public int CurrentLocationId { get; set; }

        [Required]
        public string Status { get; set; }

        public string TypeOfCar { get; set; }   

        public int? CustomerId { get; set; }

        public int? DestinationId { get; set; }

        public int? AdminId { get; set; }       // null ako je nije formirao ili obradio

        public int? DriverID { get; set; }      // null ako nema vozaca

        public int? Amount { get; set; }

        public int? CommentarId { get; set; }   // opciono sem ako je STATUS = OTKAZANA 
    }
}