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
        
        public List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>(); 
        //OBS vigtigt med en TOM liste, da OnGet fleksibelt skifter mellem alle rum, eller filtrerede rum, når vi checker boxes ud



        [BindProperty]
        public string SearchTerm { get; set; }  //medarbejder id - bruges til at sende brugeren til /Bookings

        [BindProperty(SupportsGet = true)]
        public List<string> SelectedCapacities { get; set; } = new List<string>();

        [BindProperty(SupportsGet = true)]
        public List<string> SelectedEquipment { get; set; } = new List<string>();



        public IndexModel(MeetingRoomService meetingRoomService)
        {
            _meetingRoomService = meetingRoomService;
            //GetAllMeetingRooms fjernet, da listen skal være tom indtil OnGet koden kører(se toppen af siden)
        }


        public void OnGet()
        {          
            List<MeetingRoom> allRooms = _meetingRoomService.GetAllMeetingRooms(); //standard visning- ingne filtrering

            
            if ((SelectedCapacities == null || SelectedCapacities.Count ==0) 
                && (SelectedEquipment == null|| SelectedEquipment.Count ==0))
            {
                MeetingRooms = allRooms;
                return;
            }

            //Filtrering starter, hvis checkboxes trykkes på. (OBS det er SAMME <form> tag for ALLE filtre)
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
                foreach (string equipment in SelectedEquipment)
                {

                    foreach (var item in room.Equipment)
                    {
                        if (item.ToString() == equipment)
                        {                           
                            MeetingRooms.Add(room);
                            break;
                        }
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
  
    }
}
