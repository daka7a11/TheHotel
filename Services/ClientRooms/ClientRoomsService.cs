using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.EmailSender;
using TheHotel.Mapping;
using TheHotel.ViewModels.ClientRooms;

namespace TheHotel.Services.ClientRooms
{
    public class ClientRoomsService : IClientRoomsService
    {
        private readonly IDeletableEntityRepository<ClientRoom> clientRoomsRepository;
        private readonly IMailService mailService;

        public ClientRoomsService(
            IDeletableEntityRepository<ClientRoom> clientRoomsRepository,
            IMailService mailService )
        {
            this.clientRoomsRepository = clientRoomsRepository;
            this.mailService = mailService;
        }

        public ClientRoom GetById(int id)
        {
            return clientRoomsRepository.AllWithDeleted()
                .Where(x => x.Id == id)
                .Include(x => x.Room)
                .Include(x => x.Client)
                .FirstOrDefault();
        }
        public T GetById<T>(int id)
        {
            return clientRoomsRepository.AllWithDeleted()
                .Where(x => x.Id == id)
                .Include(x => x.Room)
                .Include(x => x.Client)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task AddAsync(ClientRoom clientRoom)
        {
            await clientRoomsRepository.AddAsync(clientRoom);
            await clientRoomsRepository.SaveChangesAsync();
        }

        public ICollection<ClientRoom> GetAllReservations()
        {
            return clientRoomsRepository.All()
                .Include(x => x.Client)
                .Include(x => x.Room)
                .ToList();
        }
        public  ICollection<T> GetAllReservations<T>()
        {
            return  clientRoomsRepository.All()
                .Include(x => x.Client)
                .Include(x => x.Room)
                .To<T>()
                .ToList();
        }

        public ICollection<ClientRoom> GetConfirmedReservations()
        {
            return clientRoomsRepository.All()
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == true)
                .ToList();
        }

        public  ICollection<T> GetConfirmedReservations<T>()
        {
            return clientRoomsRepository.All()
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == true)
                .To<T>()
                .ToList();
        }

        public ICollection<ClientRoom> GetNonConfirmedReservations()
        {
            return clientRoomsRepository.All()
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == false)
                .ToList();
        }

        public ICollection<T> GetNonConfirmedReservations<T>()
        {
            return clientRoomsRepository.All()
                .Include(x => x.Client)
                .Include(x => x.Room)
                .Where(x => x.IsConfirmed == false)
                .To<T>()
                .ToList();
        }

        public async Task ConfirmRequestAsync(int id, string employeeId)
        {
            var reservationRequest = clientRoomsRepository.All()
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);
            if (reservationRequest != null)
            {
                reservationRequest.IsConfirmed = true;
                reservationRequest.EmployeeId = employeeId;
                reservationRequest.CreatedOn = System.DateTime.UtcNow;
                await clientRoomsRepository.SaveChangesAsync();
            }

            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = reservationRequest.Client.Email,
                Subject = GlobalConstants.ReservationAccepted,
                Body = "Приета",
            };
            await mailService.SendEmailAsync(mailRequest);
        }

        public async Task DeleteRequestAsync(int id, string employeeId)
        {
            var reservationRequest = clientRoomsRepository.All()
                .Include(x => x.Client)
                .FirstOrDefault(x => x.Id == id);
            if (reservationRequest == null)
            {
                return;
            }

            clientRoomsRepository.Delete(reservationRequest);
            reservationRequest.EmployeeId = employeeId;
            reservationRequest.CreatedOn = System.DateTime.UtcNow;
            await clientRoomsRepository.SaveChangesAsync();

            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = reservationRequest.Client.Email,
                Subject = GlobalConstants.ReservationDeclined,
                Body = "Отказана",
            };
            await mailService.SendEmailAsync(mailRequest);
        }

        public void Delete(int reservationId)
        {
            var reservation = clientRoomsRepository.All().FirstOrDefault(x => x.Id == reservationId);
            if (reservation != null)
            {
                clientRoomsRepository.Delete(reservation);
                clientRoomsRepository.SaveChanges();
            }
        }
    }
}
