using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingRoomService _meetingRoomService;    
        
        public List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();


        [BindProperty]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> SelectedCapacities { get; set; } = new List<string>();

        public IndexModel(MeetingRoomService meetingRoomService)
        {            
            _meetingRoomService = meetingRoomService;            
            //MeetingRooms = _meetingRoomService.GetAllMeetingRooms();

        }
        public void OnGet()
        {
            // 1. Always start by getting every single room from the service
            List<MeetingRoom> allRooms = _meetingRoomService.GetAllMeetingRooms();

            // 2. If no checkboxes are ticked, show everything and stop here
            if (SelectedCapacities == null || SelectedCapacities.Count == 0)
            {
                MeetingRooms = allRooms;
                return;
            }

            // 3. If checkboxes ARE ticked, we filter manually
            foreach (MeetingRoom room in allRooms)
            {
                foreach (string range in SelectedCapacities)
                {
                    if (range == "small" && room.Capacity >= 0 && room.Capacity <= 25)
                    {
                        MeetingRooms.Add(room);
                        break;
                    }

                    if (range == "medium" && room.Capacity > 25 && room.Capacity <= 50)
                    {
                        MeetingRooms.Add(room);
                        break;
                    }

                    if (range == "large" && room.Capacity > 50 && room.Capacity <= 75)
                    {
                        MeetingRooms.Add(room);
                        break;
                    }
                    if (range == "xlarge" && room.Capacity > 75)
                    {
                        MeetingRooms.Add(room);
                        break;
                    }
                }
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
        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
    }
}
