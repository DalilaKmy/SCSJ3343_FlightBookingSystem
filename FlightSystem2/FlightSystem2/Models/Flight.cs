//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightSystem2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        public Flight()
        {
            this.FlightBookings = new HashSet<FlightBooking>();
        }
    
        public int flightID { get; set; }
        public string flightName { get; set; }
        public string destinationFrom { get; set; }
        public string destinationTo { get; set; }
        public Nullable<System.DateTime> flightDate { get; set; }
        public Nullable<int> flightSeat { get; set; }
        public string statusAvailability { get; set; }
        public string firstClass { get; set; }
        public string secondClass { get; set; }
        public string thirdClass { get; set; }
        public Nullable<double> price { get; set; }
    
        public virtual ICollection<FlightBooking> FlightBookings { get; set; }
    }
}
