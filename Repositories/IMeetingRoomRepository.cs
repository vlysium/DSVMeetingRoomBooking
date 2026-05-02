using DSVMeetingRoomBooking.Models;

namespace DSVMeetingRoomBooking.Repositories
{
	public interface IMeetingRoomRepository
	{
		public List<MeetingRoom> GetAllMeetingRooms();
		public MeetingRoom GetMeetingRoomById(string id);
		public void AddRoom(MeetingRoom meetingRoom);
    }
}
