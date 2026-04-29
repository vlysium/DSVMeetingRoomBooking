using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace DSVMeetingRoomBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingRoomService _meetingRoomService;
        private readonly BookingService _bookingService;

        public List<MeetingRoom> MeetingRooms { get; set; }

        public Dictionary<MeetingRoom, bool> AvailableRooms { get; set; }

        // List of options to loop through in the view for filtering
        public List<(string Value, string Label)> CapacityOptions = new()
        {
            ("all", "All"),
            ("small", "0-25"),
            ("medium", "25-50"),
            ("large", "50-75"),
            ("xlarge", "75+")
        };

        // List of options to loop through in the view for filtering
        public List<(Equipment Value, string Label)> EquipmentOptions = new()
        {
            (Equipment.Projector, "Projector"),
            (Equipment.WhiteBoard, "Whiteboard"),
            (Equipment.Microphone, "Microphone")
        };

        [BindProperty]
        public string SearchTerm { get; set; }  // EmployeeId - used in `/bookings`

        [BindProperty]
        public string SelectedCapacity { get; set; } = "all"; // Default value for capacity filter

        [BindProperty]
        public List<Equipment> SelectedEquipment { get; set; } = new List<Equipment>();

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
            MeetingRooms = meetingRoomService.GetAllMeetingRooms();
            ShowRoomAvailability();
        }

        public void OnGet() { }
        
        public IActionResult OnPostSearchTerm()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Bookings", new { EmployeeId = SearchTerm });
        }

        public void OnPostFilter()
        {
            MeetingRooms = _meetingRoomService.FilterMeetingRooms(SelectedCapacity, SelectedEquipment);
            ShowRoomAvailability();
        }

        private void ShowRoomAvailability()
        {
            TimeSlot timeSlot = new TimeSlot(TimeStart, TimeEnd).FormatTimeSlot(SelectedDay, TimeStart, TimeEnd);

            AvailableRooms = new Dictionary<MeetingRoom, bool>();
            foreach (MeetingRoom room in MeetingRooms)
            {
                bool isAvailable = _bookingService.IsRoomAvailable(room.RoomId, timeSlot);
                AvailableRooms.Add(room, isAvailable);
            }
        }
    }
}
