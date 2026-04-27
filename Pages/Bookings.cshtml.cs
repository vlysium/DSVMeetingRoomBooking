using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class BookingsModel : PageModel
    {
        public List<Booking> Bookings { get; set; }
        private readonly BookingService _bookingService;
        public BookingsModel(BookingService bookingservice)
        {
            _bookingService = bookingservice;
        }
                
        public void OnGet()
        {
            
        }
        public void OnGet(string employeeId)
        {
            //Bookings = _bookingService.GetBookingsByEmployeeId(employeeId); 

            //i bookingrepository skal der laves en GetBookingsByEmployeeId så virker koden.
            //så skal en funktion skrives (foreach booking...) så brugerens MeetingRooms gøres til en liste
            //der der kan bruges til visning i bookings.cshtml            
         
        }
    }
}

