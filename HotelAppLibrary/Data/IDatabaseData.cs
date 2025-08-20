using HotelAppLibrary.Models;

namespace HotelAppLibrary.Data
{
    public interface IDatabaseData
    {
        void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInGuest(int id);
        List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        RoomTypeModel? GetRoomTypeById(int id);
        List<FullBookingModel> SearchBookings(string lastName);
    }
}