using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.EmailSender;
using TheHotel.Mapping;

namespace TheHotel.Services.ClientRooms
{
    public class ClientRoomsService : IClientRoomsService
    {
        private readonly ApplicationDbContext db;
        private readonly IMailService mailService;

        public ClientRoomsService(
            ApplicationDbContext db,
            IMailService mailService)
        {
            this.db = db;
            this.mailService = mailService;
        }

        public ClientRoom GetById(int id)
        {
            return db.ClientRooms.Where(x => x.Id == id)
                .Include(x => x.Room)
                .Include(x => x.Client)
                .FirstOrDefault();
        }
        public T GetById<T>(int id)
        {
            return db.ClientRooms.Where(x => x.Id == id)
                .Include(x => x.Room)
                .Include(x => x.Client)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task AddAsync(ClientRoom clientRoom)
        {
            await db.ClientRooms.AddAsync(clientRoom);
            await db.SaveChangesAsync();
        }

        public ICollection<ClientRoom> GetAllReservations()
        {
            return db.ClientRooms
                .Include(x => x.Client)
                .Include(x => x.Room)
                .ToList();
        }
        public ICollection<T> GetAllReservations<T>()
        {
            return db.ClientRooms
                .Include(x => x.Client)
                .Include(x => x.Room)
                .To<T>()
                .ToList();
        }

        public ICollection<ClientRoom> GetConfirmedReservations()
        {
            return db.ClientRooms
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == true)
                .ToList();
        }

        public ICollection<T> GetConfirmedReservations<T>()
        {
            return db.ClientRooms
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == true)
                .To<T>()
                .ToList();
        }

        public ICollection<ClientRoom> GetNonConfirmedReservations()
        {
            return db.ClientRooms
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == false)
                .ToList();
        }

        public ICollection<T> GetNonConfirmedReservations<T>()
        {
            return db.ClientRooms
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == false)
                .To<T>()
                .ToList();
        }

        public void ConfirmRequest(int id)
        {
            var reservationRequest = db.ClientRooms
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);
            if (reservationRequest != null)
            {
                reservationRequest.IsConfirmed = true;
                db.SaveChanges();
            }

            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = reservationRequest.Client.Email,
                Subject = GlobalConstants.ReservationAccepted,
                Body = "Приета",
            };
            mailService.SendEmailAsync(mailRequest);
        }

        public void DeleteRequest(int id)
        {
            var reservationRequest = db.ClientRooms
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);
            if (reservationRequest != null)
            {
                db.ClientRooms.Remove(reservationRequest);
                db.SaveChanges();
            }

            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = reservationRequest.Client.Email,
                Subject = GlobalConstants.ReservationDeclined,
                Body = "Отказана",
            };
            mailService.SendEmailAsync(mailRequest);
        }
    }
}
