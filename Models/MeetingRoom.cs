namespace DSVMeetingRoomBooking.Models
{

    public class MeetingRoom
	{      

        public int RoomId { get; set; }
		
        public string Name { get; set; }		

		public int Capacity { get; set; }
		public List<Equipment> Equipment { get; set; }
		public string Description { get; set; }



        public MeetingRoom()
		{
		}

		public MeetingRoom(int roomid, string nameOfRoom, int capacity, string description, List<Equipment> equipment)
		{
			RoomId = roomid;
			Name = nameOfRoom;			
			Capacity = capacity;
			Description = description;
			Equipment = equipment;
        }
	}
}
