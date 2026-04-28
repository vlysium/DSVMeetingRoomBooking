using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingRoomService _meetingRoomService;
        private readonly BookingService _bookingService;
        public List<MeetingRoom> MeetingRooms { get; set; }
        public Dictionary<MeetingRoom, bool> AvailableRooms { get; set; }

        [BindProperty]
        public string SearchTerm { get; set; }

        [BindProperty]
        public DateTime SelectedDay { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue);
        [BindProperty]
        public DateTime TimeStart { get; set; } = DateTime.Now;
        [BindProperty]
        public DateTime TimeEnd { get; set; } = DateTime.Now.AddHours(1);



        public IndexModel(MeetingRoomService meetingRoomService, BookingService bookingService)
        {            
            _meetingRoomService = meetingRoomService;
            _bookingService = bookingService;
            MeetingRooms = _meetingRoomService.GetAllMeetingRooms();

        }
        public void OnGet()
        {
            DateTime formattedTimeStart = DateTime.Parse($"{SelectedDay:dd/MM/yyyy} {TimeStart:HH:mm}");
            DateTime formattedTimeEnd = DateTime.Parse($"{SelectedDay:dd/MM/yyyy} {TimeEnd:HH:mm}");
            TimeSlot timeSlot = new TimeSlot(formattedTimeStart, formattedTimeEnd);

            AvailableRooms = new Dictionary<MeetingRoom, bool>();
            foreach (var room in MeetingRooms)
            {
                bool isAvailable = _bookingService.IsRoomAvailable(room.RoomId, timeSlot);
                AvailableRooms.Add(room, isAvailable);
            }
        }
        public IActionResult OnPostSearchTerm()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Bookings", new { EmployeeId = SearchTerm });

        }

        public void OnPostRoomAvailability()
        {
            DateTime formattedTimeStart = DateTime.Parse($"{SelectedDay:dd/MM/yyyy} {TimeStart:HH:mm}");
            DateTime formattedTimeEnd = DateTime.Parse($"{SelectedDay:dd/MM/yyyy} {TimeEnd:HH:mm}");
            TimeSlot timeSlot = new TimeSlot(formattedTimeStart, formattedTimeEnd);

            AvailableRooms = new Dictionary<MeetingRoom, bool>();
            foreach (var room in MeetingRooms)
            {
                bool isAvailable = _bookingService.IsRoomAvailable(room.RoomId, timeSlot);
                AvailableRooms.Add(room, isAvailable);
            }

        }
        
        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
    }
}
