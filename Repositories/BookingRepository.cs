using System.Text.Json;
using DSVMeetingRoomBooking.Models;

namespace DSVMeetingRoomBooking.Repositories
{
	public class BookingRepository : IBookingRepository
	{
		private readonly string path = @"Data/bookings.json";
		List<Booking> bookings = new List<Booking>();

		public BookingRepository()
		{
			if (File.Exists(path))
			{
				LoadData();
			}
		}

		/// <summary>
		/// Loads the booking data from a JSON file located at the specified path.
		/// If the file exists, it reads the content and deserializes it into a list of Booking objects.
		/// </summary>
		private void LoadData()
		{
			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				bookings = JsonSerializer.Deserialize<List<Booking>>(json)!;
			}
		}

		/// <summary>
		/// Saves the current list of bookings to a JSON file at the specified path by serializing
		/// the list of Booking objects into JSON format and writing it to the file.
		/// </summary>
		private void SaveData()
		{
			string json = JsonSerializer.Serialize(bookings);
			File.WriteAllText(path, json);
		}

		/// <summary>
		/// Adds a new booking to the list of bookings and saves the updated list to the JSON file.
		/// </summary>
		/// <param name="booking">
		/// The Booking object to be added to the list of bookings, containing details such as room ID, time slot, and optional comment.
		/// </param>
		/// <exception cref="Exception">
		/// Thrown when an error occurs while adding the booking, such as issues with data access or processing.
		/// </exception>
		public void Add(Booking booking)
		{
			try
			{
				bookings.Add(booking);
				SaveData();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while adding the booking: {ex.Message}", ex);
			}
		}

		/// <summary>
		/// Deletes a booking from the list of bookings based on the provided booking ID and saves the updated list to the JSON file.
		/// </summary>
		/// <param name="bookingId">
		/// The unique identifier of the booking to be deleted, represented as a string.
		/// </param>
		/// <exception cref="Exception">
		/// Thrown when an error occurs while deleting the booking, such as issues with data access or processing.
		/// </exception>
		public void Delete(string bookingId)
		{
			try
			{
				foreach (Booking booking in bookings)
				{
					if (booking.Id == bookingId)
					{
						bookings.Remove(booking);
						SaveData();
						return;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the booking: {ex.Message}", ex);
			}
		}

		/// <summary>
		/// Retrieves all bookings from the list of bookings and returns them as a list of Booking objects.
		/// </summary>
		/// <returns>
		/// A list of Booking objects representing all the bookings currently stored in the repository.
		/// If an error occurs while retrieving the bookings, an exception is thrown with details about the error.
		/// </returns>
		/// <exception cref="Exception">
		///	Thrown when an error occurs while retrieving the bookings, such as issues with data access or processing.
		/// </exception>
		public List<Booking> GetAll()
		{
			try
			{
				return bookings;
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving all bookings: {ex.Message}", ex);
			}
		}

		/// <summary>
		/// Retrieves a booking from the list of bookings based on the provided booking ID and returns it as a Booking object.
		/// </summary>
		/// <param name="bookingId">
		/// The unique identifier of the booking to be retrieved, represented as a string.
		/// </param>
		/// <returns>
		///	A Booking object representing the booking with the specified ID if found; otherwise, an exception is thrown indicating that the booking was not found.
		/// </returns>
		/// <exception cref="KeyNotFoundException">
		///	Thrown when a booking with the specified ID is not found in the list of bookings.
		/// </exception>
		/// <exception cref="Exception">
		///	Thrown when an error occurs while retrieving the booking, such as issues with data access or processing.
		/// </exception>
		public Booking GetById(string bookingId)
		{
			try
			{
				foreach (Booking booking in bookings)
				{
					if (booking.Id == bookingId)
					{
						return booking;
					}
				}
				throw new KeyNotFoundException($"Booking with ID {bookingId} not found.");
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the booking: {ex.Message}", ex);
			}
		}

		
		public void Update(Booking booking)
		{
			try
			{
				for (int i = 0; i < bookings.Count; i++)
				{
					if (bookings[i].Id == booking.Id)
					{
						bookings[i] = booking;
						SaveData();
						return;
					}
				}
				throw new KeyNotFoundException($"Booking with ID {booking.Id} not found.");
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the booking: {ex.Message}", ex);
			}
		}
	}
}
