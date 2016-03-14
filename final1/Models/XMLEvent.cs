using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace final1.Models
{
    public class XMLEvent
    {
        public int id { get; set; }

        //[DisplayName("EventId")]
        //[Required(ErrorMessage = "Date and number")]
        //public string eventIdentifier { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Enter event name")]
        public string eventName { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "Enter event date")]
        public string date { get; set; }

        [DisplayName("Time")]
        [Required(ErrorMessage = "Enter event time")]
        public string time { get; set; }

        [DisplayName("Location")]
        [Required(ErrorMessage = "Enter event location")]
        public string location { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Enter event description")]
        public string description { get; set; }
    }
    public class XMLEvents
    {
        string path;

        public List<XMLEvent> eventList { get; set; }

        public XMLEvents()
        {
            eventList = new List<XMLEvent>();
            path = HttpContext.Current.Server.MapPath("~\\App_Data\\XMLEvents.xml");
            Fill();
        }

        public void Fill()
        {
            try
            {
                XDocument doc = XDocument.Load(path);

                var q = from evs in doc.Elements("events").Elements("event") select evs;
                int i = 0;
                foreach (var elem in q)
                {
                    XMLEvent eve = new XMLEvent();
                    eve.id = ++i;
                    //eve.eventIdentifier = elem.Element("eventIdentifier").Value;
                    eve.eventName = elem.Element("eventName").Value;
                    eve.date = elem.Element("date").Value;
                    eve.time = elem.Element("time").Value;
                    eve.location = elem.Element("location").Value;
                    eve.description = elem.Element("description").Value;
                    eventList.Add(eve);
                }
            }
            catch
            {
                XMLEvent eve = new XMLEvent();
                eve.id = 0;
                eve.eventName = "Error";
                eventList.Add(eve);
            }
            return;
        }
        public bool Save()
        {
            try
            {
                XDocument doc = new XDocument();
                XElement elm = new XElement("events");
                foreach (XMLEvent eve in eventList)
                {
                    XElement e = new XElement("event");
                    XElement ide = new XElement("eventIdentifier");
                    //ide.Value = eve.eventIdentifier;
                    e.Add(ide);
                    XElement name = new XElement("eventName");
                    name.Value = eve.eventName;
                    e.Add(name);
                    XElement dat = new XElement("date");
                    dat.Value = eve.date;
                    e.Add(dat);
                    XElement tim = new XElement("time");
                    tim.Value = eve.time;
                    e.Add(tim);
                    XElement loc = new XElement("location");
                    loc.Value = eve.location;
                    e.Add(loc);
                    XElement des = new XElement("description");
                    des.Value = eve.description;
                    e.Add(des);
                    elm.Add(e);
                }
                doc.Add(elm);
                doc.Save(path);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}