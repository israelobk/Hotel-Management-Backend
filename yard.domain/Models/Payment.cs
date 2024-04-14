using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using yard.domain.enums;

namespace yard.domain.Models
{
    public class Payment : BaseEntity
    {
        public int AppUserId { get; set; }
        public int BookingId { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public Currency PaymentCurrency { get; set; }
        public PaymentChannel PaymentChannel { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

    }
}
