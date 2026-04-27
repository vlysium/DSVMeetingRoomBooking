namespace DSVMeetingRoomBooking.Models
{

    public class MeetingRoom
	{      

        public string RoomId { get; set; }
		
        public string Name { get; set; }		

		public int Capacity { get; set; }
		public List<Equipment> Equipment { get; set; }
		public string Description { get; set; }



        public MeetingRoom()
		{
			RoomId = Guid.NewGuid().ToString().Substring(0,8);
		}

		public MeetingRoom(string roomid, string nameOfRoom, int capacity, string description, List<Equipment> equipment)
		{
			RoomId = roomid;
			Name = nameOfRoom;			
			Capacity = capacity;
			Description = description;
			Equipment = equipment;
		}
	}
}
