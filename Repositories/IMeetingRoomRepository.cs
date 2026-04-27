using DSVMeetingRoomBooking.Models;

namespace DSVMeetingRoomBooking.Repositories
{
	public interface IMeetingRoomRepository
	{
		public List<MeetingRoom> GetAllMeetingRooms();
		public void AddRoom(MeetingRoom meetingRoom);
	
    }
}
