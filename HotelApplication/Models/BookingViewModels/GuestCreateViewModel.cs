using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApplication.Models.BookingViewModels
{
    public class GuestCreateViewModel
    {
        public Room Room { get; set; }
        [Display(Name = "Beschikbare kamers"), Required]
        public int RoomID { get; set; }

        [Display(Name = "From: ")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "To: ")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime EndDateTime { get; set; }

    }
}
