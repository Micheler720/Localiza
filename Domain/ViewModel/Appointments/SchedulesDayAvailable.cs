using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.ViewModel.Appointments
{
    public class SchedulesDayAvailable
    {
        public int IdAppointment { get; set; }
        public DateTime Schedule { get; set; }
        public string NameClient { get; set; }
        public string[] Images { get; set; }

        [JsonIgnore]
        public string Photos { get; set; }
        public string Board { get; set; }
        public Double Amount { get; set; }
        public DateTime AppointmentCollected { get; set; }
        public DateTime AppointmentDelivery { get; set; }
    }
}
