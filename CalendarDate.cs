using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XYZ.CalendarHelper
{
    public class CalendarDate
    {
        private readonly DateTime _date;
        /// <summary>
        /// Instantiates a new CalendarDate object using the default DateTime constructor.
        /// </summary>
        public CalendarDate() {
            _date = new DateTime();
        }
        /// <summary>
        /// Instantiates a new CalendarDate object using the provided DateTime
        /// </summary>
        /// <param name="date">The DateTime value to populate the object</param>
        /// <param name="displayColor">(optional) The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.</param>
        /// <param name="callbackFunction">(optional) A javascript callback function that will fire when the event is clicked.</param>
        public CalendarDate(DateTime date, String displayColor = "#999999", String callbackFunction = null, int displayOrder = 10)
        {
            _date = date;
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
            DisplayOrder = displayOrder;
        }
        /// <summary>
        /// Instantiates a new CalendarDate object using ticks
        /// </summary>
        /// <param name="ticks">The ticks to represent the date value</param>
        /// <param name="displayColor">(optional) The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.</param>
        /// <param name="callbackFunction">(optional) A javascript callback function that will fire when the event is clicked.</param>
        public CalendarDate(long ticks, String displayColor = "#999999", String callbackFunction = null, int displayOrder = 10)
        {
            _date = new DateTime(ticks);
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
            DisplayOrder = displayOrder;
        }
        /// <summary>
        /// Instantiates a new CalendarDate object
        /// </summary>
        /// <param name="ticks">The ticks to represent the date value</param>
        /// <param name="kind">Specifies whether the date represented is local, UTC, or neither</param>
        /// <param name="displayColor">(optional) The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.</param>
        /// <param name="callbackFunction">(optional) A javascript callback function that will fire when the event is clicked.</param>
        public CalendarDate(long ticks, DateTimeKind kind, String displayColor = "#999999", String callbackFunction = null, int displayOrder = 10)
        {
            _date = new DateTime(ticks, kind);
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
            DisplayOrder = displayOrder;
        }
        /// <summary>
        /// Instantiates a new CalendarDate object using the year, month and day.
        /// </summary>
        /// <param name="year">The year to represent</param>
        /// <param name="month">The month to represent</param>
        /// <param name="day">The day to represent</param>
        /// <param name="displayColor">(optional) The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.</param>
        /// <param name="callbackFunction">(optional) A javascript callback function that will fire when the event is clicked.</param>
        public CalendarDate(int year, int month, int day, String displayColor = "#999999", String callbackFunction = null, int displayOrder = 10)
        {
            _date = new DateTime(year, month, day);
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
            DisplayOrder = displayOrder;
        }
        /// <summary>
        /// Instantiates a new CalendarDate object using the year, month and day
        /// </summary>
        /// <param name="year">The year to represent</param>
        /// <param name="month">The month to represent</param>
        /// <param name="day">The day to represent</param>
        /// <param name="calendar">Represents time in divisions such as weeks, months and years</param>
        /// <param name="displayColor">(optional) The color used to represent the event.  Can be in either "#XXXXXXX" or "rgba(XXX,XXX,XXX,XXX)" formats.</param>
        /// <param name="callbackFunction">(optional) A javascript callback function that will fire when the event is clicked.</param>
        public CalendarDate(int year, int month, int day, System.Globalization.Calendar calendar, String displayColor = "#999999", String callbackFunction = null, int displayOrder = 10)
        {
            _date = new DateTime(year, month, day, calendar);
            DisplayColor = displayColor;
            CallbackFunction = callbackFunction;
            DisplayOrder = displayOrder;
        }
        /// <summary>
        /// The Current Date
        /// </summary>
        public static CalendarDate Now
        {
            get { return new CalendarDate(DateTime.Now); }
        }
        /// <summary>
        /// The underlying DateTime object.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return _date.Date;
            }
        }
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
    }
}
