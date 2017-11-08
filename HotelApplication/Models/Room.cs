using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApplication.Models
{
    public class Room
    {
        [Display(Name = "ID kamer")]
        public int RoomID { get; set; }

        [Display(Name = "Kamer naam")]
        public string RoomName { get; set; }

        [Display(Name = "Aantal personen"), Range(1, 10), Required]
        public int RoomSize { get; set; }

        [Display(Name = "Prijs per dag"), DataType(DataType.Currency), Required]
        public decimal RoomPrice { get; set; }

        [Display(Name = "Beschikbaar")]
        public bool RoomAvailability { get; set; }
    }
}
