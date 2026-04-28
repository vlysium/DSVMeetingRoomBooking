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
        
        public List<MeetingRoom> MeetingRooms { get; set; }

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

        public IndexModel(MeetingRoomService meetingRoomService)
        {
            _meetingRoomService = meetingRoomService;
            MeetingRooms = meetingRoomService.GetAllMeetingRooms();
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
            Console.WriteLine($"Selected Capacity: {SelectedCapacity}");
            Console.WriteLine($"Selected Equipment: {string.Join(", ", SelectedEquipment)}");
            MeetingRooms = _meetingRoomService.FilterMeetingRooms(SelectedCapacity, SelectedEquipment);
        }
    }
}
