using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class BookingsModel : PageModel
    {
        public List<Booking> Bookings { get; set; }
        public List<MeetingRoom> MeetingRooms { get; set; }
        private readonly BookingService _bookingService;
        private readonly MeetingRoomService _meetingRoomService;
        public BookingsModel(BookingService bookingservice, MeetingRoomService meetingRoomService)
        {
            _bookingService = bookingservice;
            _meetingRoomService = meetingRoomService;
            Bookings = _bookingService.GetAllBookings();
            MeetingRooms = _meetingRoomService.GetAllMeetingRooms();
        }

        public void OnGet(string employeeId)
        {
            if (!string.IsNullOrEmpty(employeeId))
            {
                Bookings = _bookingService.GetBookingsByEmployeeId(employeeId);
            }

            //i bookingrepository skal der laves en GetBookingsByEmployeeId så virker koden.
            //så skal en funktion skrives (foreach booking...) så brugerens MeetingRooms gøres til en liste
            //der der kan bruges til visning i bookings.cshtml
            
         
        }
    }
}

