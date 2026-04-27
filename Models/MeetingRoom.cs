namespace DSVMeetingRoomBooking.Models
{
	public class MeetingRoom
	{

		private int _RoomId { get; set; }
		
        private string _NameOfRoom { get; set; }

		private bool _IsAvailable { get; set; }

		private int _Capacity { get; set; }


		public MeetingRoom()
		{



		}

		//public MeetingRoom ( int roomid , string nameOfRoom , bool isavailble , int capacity)
		//{
		//	RoomId = roomid;
		//	nameOfRoom = nameOfRoom;
		//	IsAvailable = isavailble;
		//	Capacity = capacity;



		//}
    }
}
