using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.enums;
using yard.domain.Models;

namespace yard.domain.ViewModels
{
    public class TransactionVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string HotelName { get; set; }
        public string RoomTypeName {  get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string BookingReference { get; set; }
        public PaymentChannel PaymentChannel { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
