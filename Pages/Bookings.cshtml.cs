using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class BookingsModel : PageModel
    {
        public List<Booking> bookings;
        private readonly BookingService _bookingservice;
        public BookingsModel(BookingService bookingservice)
        {
            _bookingservice = bookingservice;
        }

       
        public void OnGet()
        {
            //bookings= _bookingservice.FILTRETINGSfunktion();
        }
    }
}
