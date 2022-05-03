using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.ClientRooms
{
    public interface IClientRoomsService
    {
        ClientRoom GetById(int id);
        T GetById<T>(int id);

        Task AddAsync(ClientRoom clientRoom);

        ICollection<ClientRoom> GetAllReservations();
        ICollection<T> GetAllReservations<T>();


        ICollection<ClientRoom> GetConfirmedReservations();
        ICollection<T> GetConfirmedReservations<T>();

        ICollection<ClientRoom> GetNonConfirmedReservations();
        ICollection<T> GetNonConfirmedReservations<T>();

        void ConfirmRequest(int id);

        void DeleteRequest(int id);
    }
}
