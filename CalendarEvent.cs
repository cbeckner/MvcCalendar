using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XYZ.CalendarHelper
{
    public class CalendarEvent
    {
        public CalendarEvent(DateTime startDate, DateTime endDate, String displayColor = "#999999", String callbackFunction = null)
        {
            StartDate = startDate;
            EndDate = endDate;
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String DisplayColor { get; set; }
        public String CallbackFunction { get; set; }
        public IEnumerable<DateTime> Dates
        {
            get
            {
                return Enumerable.Range(0, (int)(this.EndDate - this.StartDate).TotalDays + 1).Select(d => this.StartDate.AddDays(d).Date);
            }
        }

        public bool ContainsDate(DateTime date)
        {
            return Dates.Contains(date.Date);
        }
    }
}
