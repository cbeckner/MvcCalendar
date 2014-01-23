Asp.Net MVC Calendar Html Helper
================================
An inline calendar display HTML Helper for ASP.Net MVC

##Using
Using it can be as simple as:
```asp
<%=Html.Calendar() %>
```
or
```asp
@Html.Calendar()
```

That's It!

##Additional Options
I am working to make the control as extensible as possible for the various scenarios that it will hopefully be used for.  As such, there are several overloads that you can take advantage of.

You can specify the month that you want the calendar to display by passing in a *DateTime* object.
```asp
@Html.Calendar(DateTime.Now)
```

##Events

You can also fill the calendar with events by passing in a list of *CalendarEvent* objects.
```asp
@{
  var events = new List<CalendarEvent>();
  events.Add(new CalendarEvent(new DateTime(2014, 1, 15), new DateTime(2014, 1, 20), "#333333", "viewDate(12345"));     
}
@=Html.Calendar(DateTime.Now,events)
```

The *CalendarEvent* object takes the following arguments:
* **StartDate** : The date the event begins.
* **EndDate** : The date the event ends.
* **DisplayColor** : The color that will be applied to the event when it is placed on the calendar. (optional. defaults to #999999)
* **CallbackFunction** : A javascript function that will be called when the event is clicked on. (optional)

Additionally, the *CalendarEvent* class exposes two functions.
* **Dates** : Enumerates all of the dates included in the event to include the start and end date.
* **ContainsDate(DateTime)** : Returns true/false depending whether the provided date is in the range of Dates.

##Styling
There is an additional helper that will provide some out of the box styling for the calendar.  This should be called prior to calling the first calendar on your page.  If you want to override one or two of the styles, I would suggest calling the css helper in the head of the site and then overriding the classes in your pages stylesheet.

To call the CSS control
```asp
@Html.CalendarCss()
```

The CSS exposes the following classes:
* **.cal-body** : This styles the table element for the calendar
* **.cal-body th** : The style for the header elements.  This will define how the days of the week are displayed.
* **.cal-body td** : The style for the dates.  This will define alignment and cell size.
* **.cal-body a** : How any callback links should be styled.  
* **.cal-x** : How empty (non-date) cells should be rendered.
* **.cal-ar** : How the end date will be rendered. By default it is an triangle.
* **.cal-al** : How the start date will be rendered.  By default it is a triangle.

Copyright 2014 by Cody Beckner!
