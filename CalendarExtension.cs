using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XYZ.CalendarHelper
{
    /// <summary>
    /// The MVC Calendar Extension for HtmlHelper
    /// </summary>
    public static class CalendarExtension
    {
        private const string Style = @"
            <style type='text/css'>
            .cal-body {border-spacing: 3px; border-collapse: separate; margin: 10px 20px 10px 15px;}
                .cal-body th {color: #666;border-bottom: 1px solid #ddd;}
                .cal-body td {text-align: center;}
                .cal-body td div { position:relative; padding: 5px 5px 2px;}
                .cal-body a {text-decoration:none; color:#000;}
            .cal-body td div div.cal-ar {width: 0; height: 0; border-bottom: 18px solid transparent;border-left: 18px solid;position:absolute;top:0;left:0; padding:0}
            .cal-body td div div.cal-al {width: 0; height: 0; border-top: 18px solid transparent;border-right:18px solid; position:absolute;bottom:0;right:0; padding:0}
            .cal-x {background-color: transparent;}
            </style>
            ";

        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString CalendarCss(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(Style);
        }
        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender)
        {
            return Calendar(helper, monthToRender, new List<CalendarEvent>(), new List<CalendarDate>(), null);
        }
        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <param name="beforeDayRender">A function to customize the day cell before it is rendered</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, Action<DateTime, TagBuilder, TagBuilder> beforeDayRender)
        {
            return Calendar(helper, monthToRender, new List<CalendarEvent>(), new List<CalendarDate>(), null, BeforeDayRender: beforeDayRender);
        }
        
        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <param name="htmlAttributes">Optional attributes that will be applied to the parent table of the calendar</param>
        /// <param name="beforeDayRender">A function to customize the day cell before it is rendered</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, Action<DateTime, TagBuilder, TagBuilder> beforeDayRender, object htmlAttributes)
        {
            return Calendar(helper, monthToRender, new List<CalendarEvent>(), new List<CalendarDate>(), htmlAttributes: htmlAttributes, BeforeDayRender: beforeDayRender);
        }

        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <param name="events">A list of events to display on the calendar.</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<CalendarEvent> events)
        {
            return Calendar(helper, monthToRender, events, new List<CalendarDate>(), null);
        }
        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <param name="dates">A list of individual dates to display on the calendar.</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<CalendarDate> dates)
        {
            return Calendar(helper, monthToRender, new List<CalendarEvent>(), dates, null);
        }
        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <param name="dates">A list of individual dates to display on the calendar.</param>
        /// <param name="htmlAttributes">Optional attributes that will be applied to the parent table of the calendar</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<CalendarDate> dates, object htmlAttributes)
        {
            return Calendar(helper, monthToRender, new List<CalendarEvent>(), dates, null);
        }
        /// <summary>
        /// Generates HTML for a calendar control
        /// </summary>
        /// <param name="helper">The HtmlHelper being extended</param>
        /// <param name="monthToRender">A DateTime object representing the month that should be displayed</param>
        /// <param name="events">A list of events to display on the calendar.</param>
        /// <param name="htmlAttributes">Optional attributes that will be applied to the parent table of the calendar</param>
        /// <returns>HTML to display a calendar</returns>
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<CalendarEvent> events, object htmlAttributes)
        {
            return Calendar(helper, monthToRender, events, new List<CalendarDate>(), htmlAttributes: htmlAttributes);
        }

        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<CalendarDate> dates, string defaultCallbackFunction, string defaultTooltip, object htmlAttributes)
        {
            return Calendar(helper, monthToRender, new List<CalendarEvent>(), dates, defaultCallbackFunction, defaultTooltip, htmlAttributes);
        }

        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<CalendarEvent> events, string defaultCallbackFunction, string defaultTooltip, object htmlAttributes)
        {
            return Calendar(helper, monthToRender, events, new List<CalendarDate>(), defaultCallbackFunction, defaultTooltip, htmlAttributes);
        }

        private static MvcHtmlString Calendar(HtmlHelper helper,
            DateTime monthToRender,
            List<CalendarEvent> events = null,
            List<CalendarDate> dates = null,
            string defaultCallbackFunction = null,
            string defaultTooltip = null,
            object htmlAttributes = null, 
            Action<DateTime, TagBuilder, TagBuilder> BeforeDayRender = null)
        {
            TagBuilder calendar = new TagBuilder("table");
            calendar.Attributes.Add("class", "cal-body");

            calendar.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            if (events == null) events = new List<CalendarEvent>();

            TagBuilder monthName = new TagBuilder("caption");
            monthName.Attributes.Add("style", "font-weight:bold");
            monthName.Attributes.Add("class", "monthname");
            monthName.SetInnerText(monthToRender.ToString("MMMM yyyy"));
            calendar.InnerHtml = monthName.ToString();

            //Build Day Names
            TagBuilder dayNames = new TagBuilder("tr");
            var days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            var daysShort = new[] { "M", "Tu", "W", "Th", "F", "Sa", "Su" };
            var daysLong = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            for (int i = 0; i < 7; i++)
            {
                TagBuilder dayHeader = new TagBuilder("th");
                dayHeader.Attributes.Add("abbr", days[i]);
                dayHeader.Attributes.Add("title", daysLong[i]);
                dayHeader.SetInnerText(daysShort[i]);
                dayNames.InnerHtml += dayHeader.ToString();
            }

            //Build Calendar
            List<TagBuilder> monthRows = new List<TagBuilder>();
            TagBuilder row = new TagBuilder("tr");
            int columnCount = 0;
            CalendarEvent previousMatch = null;
            for (int i = 1; i <= DateTime.DaysInMonth(monthToRender.Year, monthToRender.Month); i++)
            {
                DateTime day = new DateTime(monthToRender.Year, monthToRender.Month, i);                

                if (i == 1)
                {
                    foreach (string dow in days)
                    {
                        if (dow.Equals(day.DayOfWeek.ToString().Substring(0, 3), StringComparison.OrdinalIgnoreCase)) break;
                        TagBuilder blankTag = new TagBuilder("td");
                        blankTag.Attributes.Add("class", "cal-x");
                        row.InnerHtml += blankTag.ToString();
                        columnCount++;
                    }
                }
                TagBuilder dayTagWrapper = new TagBuilder("td");
                TagBuilder dayTag = new TagBuilder("div");                
                //See if there is a match
                var matches = events.Where(b => b.ContainsDate(day));
                if (matches.Any())
                {
                    var match = matches.OrderBy(d => d.DisplayOrder).First();
                    if ((match.EndDate.Date.Equals(day) || 
                        ((previousMatch != null) && previousMatch.DisplayColor != match.DisplayColor))
                        && match.AngledStartEnd)
                    {
                        if (match.EndDate.Date.Equals(day))
                            dayTag.InnerHtml = "<div class=\"cal-ar\" style=\"border-left-color:" + match.DisplayColor + "\"></div>";
                        else
                            dayTag.InnerHtml = "<div class=\"cal-ar\" style=\"border-left-color:" + previousMatch.DisplayColor + "\"></div>";
                        dayTag.Attributes.Remove("style");
                        if(previousMatch != null)
                            dayTag.InnerHtml += "<div class=\"cal-al\" style=\"border-right-color:" + previousMatch.DisplayColor + "\"></div>";
                        previousMatch = null;
                    }
                    else
                    {
                        dayTag.Attributes.Add("style", "background-color:" + match.DisplayColor);
                        previousMatch = match;
                    }

                    if (!String.IsNullOrEmpty(match.CallbackFunction))
                        dayTag.InnerHtml += String.Format("<a href=\"javascript:void(0)\" title=\"{2}\" onclick=\"{0}\">{1}</a>", match.CallbackFunction, i.ToString(), match.Tooltip);
                    else
                        dayTag.InnerHtml += "<a title=\"" + match.Tooltip + "\">" + i.ToString() + "</a>";

                    if ((match.StartDate.Date.Equals(day) ||
                        ((previousMatch !=null) && day == previousMatch.EndDate && 
                        (day > match.StartDate && day < match.EndDate)))
                        && match.AngledStartEnd)
                    {
                        dayTag.InnerHtml += "<div class=\"cal-al\" style=\"border-right-color:" + match.DisplayColor + "\"></div>";
                        dayTag.Attributes.Remove("style");
                    }
                }
                else
                {
                    if (dates.Any(d => d.Date == day.Date))
                    {
                        previousMatch = null;
                        var singleMatch = dates.First(d => d.Date == day.Date);
                        dayTag.Attributes.Add("style", "background-color:" + singleMatch.DisplayColor);
                        if (!String.IsNullOrEmpty(singleMatch.CallbackFunction))
                            dayTag.InnerHtml += String.Format("<a href=\"javascript:void(0)\" title=\"{2}\" onclick=\"{0}\">{1}</a>", singleMatch.CallbackFunction, i.ToString(), singleMatch.Tooltip);
                        else
                            dayTag.InnerHtml += "<a title=\"" + singleMatch.Tooltip + "\">" + i.ToString() + "</a>";
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(defaultCallbackFunction))
                        dayTag.SetInnerText(i.ToString());
                        else
                        {
                            if (string.IsNullOrEmpty(defaultTooltip))
                            {
                                dayTag.InnerHtml += "<a onclick=\"" + defaultCallbackFunction + "\">" + i.ToString() + "</a>";
                            }
                            else
                            {
                                dayTag.InnerHtml += "<a title=\"" + defaultTooltip + "\" onclick=\"" + defaultCallbackFunction + "\">" + i.ToString() + "</a>";
                            }
                        }
                    }
                }
                BeforeDayRender(day, dayTagWrapper, dayTag);
                dayTagWrapper.InnerHtml = dayTag.ToString();
                row.InnerHtml += dayTagWrapper.ToString();
                columnCount++;
                if (columnCount == 7)
                {
                    monthRows.Add(row);
                    row = new TagBuilder("tr");
                    columnCount = 0;
                }
            }
            while (columnCount < 7)
            {
                TagBuilder blankTag = new TagBuilder("td");
                blankTag.Attributes.Add("class", "cal-x");
                row.InnerHtml += blankTag.ToString();
                columnCount++;
            }
            monthRows.Add(row);

            calendar.InnerHtml += dayNames.ToString();
            foreach (var daysrow in monthRows)
            {
                calendar.InnerHtml += daysrow.ToString();
            }
            return MvcHtmlString.Create(calendar.ToString(TagRenderMode.Normal));
        }
    }
}
