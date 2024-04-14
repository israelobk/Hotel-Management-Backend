using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Context;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.infrastructure.Repositories.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _context;

        public TransactionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<TransactionVM> GetAllSuccessfulTransaction()
        {
            var query = from payment in _context.Payments
                        join booking in _context.Bookings on payment.BookingId equals booking.Id
                        join appUser in _context.Users on booking.AppUserId equals appUser.Id
                        join hotel in _context.Hotels on booking.HotelId equals hotel.Id
                        join room in _context.Rooms on booking.RoomId equals room.Id
                        join roomType in _context.RoomTypes on room.RoomType.Id equals roomType.Id
                        where payment.PaymentStatus == domain.enums.PaymentStatus.Success &&
                              payment.CreatedAt > new DateTime(2021, 01, 01) &&
                              payment.CreatedAt < new DateTime(2024, 01, 01)
                        select new TransactionVM
                        {
                            FirstName = appUser.FirstName,
                            LastName = appUser.LastName,
                            HotelName = hotel.Name,
                            RoomTypeName = roomType.Name,
                            Amount = payment.Amount,
                            Reference = payment.Reference,
                            BookingReference = booking.BookingReference,
                            PaymentChannel = payment.PaymentChannel,
                            CreateAt = payment.CreatedAt
                        };


            return  query;

        }
    }
}
