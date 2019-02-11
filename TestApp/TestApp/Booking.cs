using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestApp
{
    [DataContract]
    public class Booking
    {
        [DataMember]
        public int BookingID { get; set; }
        [DataMember]
        public Customer Customer{ get; set; }
        [DataMember]
        public Hotel Hotel { get; set; }
        [DataMember]
        public DateTime DateBookingMade{ get; set; }
        [DataMember]
        public DateTime DateOfBooking { get; set; }
        [DataMember]
        public bool Activated { get; set; }
        [DataMember]
        public bool HideBooking { get; set; }

        public Booking() { }
    }
}
