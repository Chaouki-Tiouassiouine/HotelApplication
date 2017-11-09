using System;
using System.ComponentModel.DataAnnotations;

namespace HotelApplication.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        [Display(Name = "Aankomst"), DataType(DataType.Date), Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "Vertrek"), DataType(DataType.Date), Required]
        public DateTime EndDate { get; set; }

        public Room Room { get; set; }
        [Display(Name = "Beschikbare kamers"), Required]
        public int RoomID { get; set; }

        public ApplicationUser Guest { get; set; }
        [Display(Name = "Naam van gast"), Required]
        public string GuestId { get; set; }

        [Display(Name = "Totale prijs")]
        public double TotalPrice { get; set; }

        [Display(Name = "Betaald")]
        public bool PaidBooking { get; set; }

    }
}
