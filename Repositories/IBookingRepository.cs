using DSVMeetingRoomBooking.Models;

namespace DSVMeetingRoomBooking.Repositories
{
	public interface IBookingRepository
	{
		List<Booking> GetAll();
		Booking GetById(string bookingId);
		void Add(Booking booking);
		void Update(Booking booking);
		void Delete(string bookingId);
	}
}
