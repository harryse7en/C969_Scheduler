using System;

namespace Scheduler.Classes
{
    public class Appointment
    {
        // ---------- Variables ----------
        public int appointmentId { get; set; }
        public int customerId { get; set; }
        public int userId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
