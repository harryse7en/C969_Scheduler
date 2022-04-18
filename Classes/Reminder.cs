using System;

namespace Scheduler.Classes
{
    public class Reminder
    {
        // ---------- Variables ----------
        public int ID;
        public string Name, Type;
        public bool Triggered;
        public DateTime RemindTime, StartTime, EndTime;
    }
}
