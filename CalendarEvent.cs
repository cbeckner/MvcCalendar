using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XYZ.CalendarHelper
{
    /// <summary>
    /// Defines an Event to be displayed on the Calendar Control
    /// </summary>
    public class CalendarEvent
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="startDate">The Event Start Date</param>
        /// <param name="endDate">The Event End Date</param>
        /// <param name="displayColor">(optional) The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.</param>
        /// <param name="callbackFunction">(optional) A javascript callback function that will fire when the event is clicked.</param>
        /// <param name="angledStartEnd">(optional) Determines whether an angle should be used to represent the start and end of an event</param>
        public CalendarEvent(DateTime startDate, DateTime endDate, String tooltip = "", String displayColor = "#999999", String callbackFunction = null, bool angledStartEnd = true, int displayOrder = 10)
        {
            StartDate = startDate;
            EndDate = endDate;
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
            AngledStartEnd = angledStartEnd;
            DisplayOrder = displayOrder;
            Tooltip = tooltip;
        }
        /// <summary>
        /// The Event Start Date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// The Event End Date
        /// </summary>
        public DateTime EndDate { get; set; }        
        /// <summary>
        /// Determines whether an angle should be used to represent the start and end of an event
        /// </summary>
        public bool AngledStartEnd { get; set; }
        /// <summary>
        /// The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.
        /// </summary>
        public String DisplayColor { get; set; }
        /// <summary>
        /// A javascript callback function that will fire when the event is clicked.
        /// </summary>
        public String CallbackFunction { get; set; }
        /// <summary>
        /// Determines the order of preference if multiple matches exist for a single date.  Lower numbers get higher priority.
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// An enumeration of all the dates represented by this event.  Includes Start and End dates.
        /// </summary>
        public IEnumerable<DateTime> Dates
        {
            get
            {
                return Enumerable.Range(0, (int)(this.EndDate - this.StartDate).TotalDays + 1).Select(d => this.StartDate.AddDays(d).Date);
            }
        }
        /// <summary>
        /// Determins if a given date is contained in the event.
        /// </summary>
        /// <param name="date">The Date to search for</param>
        /// <returns>True/False</returns>
        public bool ContainsDate(DateTime date)
        {
            return Dates.Contains(date.Date);
        }
        /// <summary>
        /// Text that will be added to the title element for the event
        /// </summary>
        public string Tooltip { get; set; }
    }
}
