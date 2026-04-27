using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using System.Text.Json;

namespace DSVMeetingRoomBooking.Repositories
{
	public class MeetingRoomRepository: IMeetingRoomRepository
    {
		private string _filePath = @"Data/rooms.json";

		private List<MeetingRoom> _meetingRooms;

		public List<MeetingRoom> GetAllMeetingRooms()
		{
			LoadFile();
			return _meetingRooms;
		}

        public void LoadFile()
		{
			string json = File.ReadAllText(_filePath);
			_meetingRooms = JsonSerializer.Deserialize<List<MeetingRoom>>(json);		
        }
		public void SaveFile()
		{
			string json = JsonSerializer.Serialize(_meetingRooms);
            File.WriteAllText(_filePath, json);
        }
       

        public void AddRoom(MeetingRoom meetingRoom)
		{
			LoadFile();
			_meetingRooms.Add(meetingRoom);
			SaveFile();
		}
    }
}
