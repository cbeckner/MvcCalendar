using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XYZ.CalendarHelper
{
    public static class CalendarExtension
    {
        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender)
        {
            return Calendar(helper, monthToRender, new List<DateTime>(), null);
        }

        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, object htmlAttributes)
        {
            return Calendar(helper, monthToRender, new List<DateTime>(), htmlAttributes);
        }

        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<DateTime> blockedDates)
        {
            return Calendar(helper, monthToRender, blockedDates,null);
        }

        public static MvcHtmlString Calendar(this HtmlHelper helper, DateTime monthToRender, List<DateTime> blockedDates, object htmlAttributes)
        {
            TagBuilder calendar = new TagBuilder("table");
            calendar.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            
            if (blockedDates == null) blockedDates = new List<DateTime>();
            blockedDates = blockedDates.Select(d => d.Date).ToList();

            TagBuilder header = new TagBuilder("tr");
            TagBuilder monthName = new TagBuilder("td");
            monthName.Attributes.Add("style", "font-weight:bold");
            monthName.Attributes.Add("class", "monthname");
            monthName.Attributes.Add("colspan", "7");
            monthName.SetInnerText(monthToRender.ToString("MMMM"));
            header.InnerHtml = monthName.ToString();

            //Build Day Names
            TagBuilder dayNames = new TagBuilder("tr");
            var days = new [] {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat","Sun"};
            for(int i = 0;i<7;i++) {
                TagBuilder dayHeader = new TagBuilder("th");
                dayHeader.SetInnerText(days[i]);
                dayNames.InnerHtml += dayHeader.ToString();
            }

            //Build Calendar
            List<TagBuilder> monthRows = new List<TagBuilder>();
            TagBuilder row = new TagBuilder("tr");
            int columnCount = 0;
            for (int i = 1; i <= DateTime.DaysInMonth(monthToRender.Year, monthToRender.Month);i++ )
            {
                DateTime day = new DateTime(monthToRender.Year, monthToRender.Month, i);
                if (i == 1)
                {
                    foreach (string dow in days)
                    {
                        if (dow.Equals(day.DayOfWeek.ToString().Substring(0, 3), StringComparison.OrdinalIgnoreCase)) break;
                        TagBuilder blankTag = new TagBuilder("td");
                        blankTag.Attributes.Add("class", "disabled");
                        row.InnerHtml += blankTag.ToString();
                        columnCount++;
                    }
                }
                TagBuilder dayTag = new TagBuilder("td");
                if (blockedDates.Contains(day))
                    dayTag.Attributes.Add("class", "blocked");

                dayTag.SetInnerText(i.ToString());
                row.InnerHtml += dayTag.ToString();
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
                blankTag.Attributes.Add("class", "disabled");
                row.InnerHtml += blankTag.ToString();
                columnCount++;
            }
            monthRows.Add(row);

            calendar.InnerHtml = header.ToString();
            calendar.InnerHtml += dayNames.ToString();
            foreach(var daysrow in monthRows) {
                calendar.InnerHtml += daysrow.ToString();
            }
            return MvcHtmlString.Create(calendar.ToString(TagRenderMode.Normal)); 
        }
    }
}
