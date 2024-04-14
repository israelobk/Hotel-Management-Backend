using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace yard.domain.Models
{
    public class Room : BaseEntity
    {
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }

        [JsonIgnore]
        public RoomType RoomType { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}

