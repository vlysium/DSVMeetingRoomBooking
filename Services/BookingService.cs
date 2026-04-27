using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Repositories;

namespace DSVMeetingRoomBooking.Services
{
	public class BookingService
	{
		private readonly IBookingRepository _bookingRepository;

		public BookingService(IBookingRepository bookingRepository)
		{
			_bookingRepository = bookingRepository;
		}

		// public bool IsRoomAvailable(int roomId, TimeSlot timeslot) {}

		/// <summary>
		/// Gets all bookings from the repository.
		/// </summary>
		/// <returns>
		///	A list of all bookings.
		/// </returns>
		public List<Booking> GetAllBookings()
		{
			return _bookingRepository.GetAll();
		}

		/// <summary>
		/// Gets a specific booking by its ID from the repository.
		/// </summary>
		/// <param name="bookingId">
		///	The ID of the booking to retrieve.
		/// </param>
		/// <returns>
		///	The booking with the specified ID, or null if not found.
		/// </returns>
		public Booking GetBooking(string bookingId)
		{
			return _bookingRepository.GetById(bookingId);
		}

		public List<Booking> GetBookingsByEmployeeId(string employeeId)
		{
			List<Booking> allBookings = _bookingRepository.GetAll();
			List<Booking> employeeBookings = new List<Booking>();
			for (int i = 0; i < allBookings.Count; i++)
			{
				if (allBookings[i].EmployeeId == employeeId)
				{
					employeeBookings.Add(allBookings[i]);
				}
			}
			return employeeBookings;
		}

		/// <summary>
		/// Creates a new booking and adds it to the repository.
		/// </summary>
		/// <param name="booking">
		///	The booking to create and add to the repository.
		/// </param>
		public void CreateBooking(Booking booking)
		{
			_bookingRepository.Add(booking);
		}

		/// <summary>
		/// Updates an existing booking in the repository.
		/// </summary>
		/// <param name="booking">
		///	The booking to update in the repository. The booking must already exist in the repository.
		/// </param>
		public void UpdateBooking(Booking booking)
		{
			_bookingRepository.Update(booking);
		}

		/// <summary>
		/// Deletes a booking from the repository by its ID.
		/// </summary>
		/// <param name="bookingId">
		///	The ID of the booking to delete from the repository.
		/// </param>
		public void DeleteBooking(string bookingId)
		{
			_bookingRepository.Delete(bookingId);
		}
	}
}
