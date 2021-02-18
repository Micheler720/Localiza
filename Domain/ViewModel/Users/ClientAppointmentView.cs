using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel.Users
{
    public class ClientAppointmentView
    {
        public int Id { get; set; }
        public DateTime Schedule { get; set; }
        public string NameClient { get; set; }
        public string Board { get; set; }
        public string Category { get; set; }
        public Double HourPrice { get; set; }
        public int HourLocation { get; set; }
        public Double Subtotal { get; set; }
        public Double AdditionalCosts { get; set; }
        public DateTime AppointmentCollected { get; set; }
        public DateTime AppointmentDelivery { get; set; }
    }
}
