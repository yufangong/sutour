using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final1.Controllers
{
    [Authorize(Roles="admin")]
    public class XMLEventController : Controller
    {
        //
        // GET: /XMLEvent/
        [AllowAnonymous]
        public ActionResult Events(Models.XMLEvents events)
        {
            return View(events.eventList);
        }
        [AllowAnonymous]
        public ActionResult Day(string date)
        {
            Models.XMLEvents events = new Models.XMLEvents();
            List<Models.XMLEvent> dayeve = new List<Models.XMLEvent>();
            foreach(var eve in events.eventList)
            {
                if(eve.date == date)
                {
                    dayeve.Add(eve);
                }

            }
            return View(dayeve);
        }
        [AllowAnonymous]
         public ActionResult Details(int id)
        {
            try
            {
                Models.XMLEvents eves = new Models.XMLEvents();
                foreach (Models.XMLEvent eve in eves.eventList)
                {
                    if (eve.id == id)
                    {
                        return View(eve);
                    }
                }
                return RedirectToAction("Events");
            }
            catch
            {
                return RedirectToAction("Events");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.XMLEvents events = new Models.XMLEvents();
                Models.XMLEvent eve = new Models.XMLEvent();
                eve.id = events.eventList.Count;
                //eve.eventIdentifier = collection["eventIdentifier"];
                eve.eventName = collection["eventName"];
                eve.date = collection["date"];
                eve.time = collection["time"];
                eve.location = collection["location"];
                eve.description = collection["description"];

                events.eventList.Add(eve);
                events.Save();

                return RedirectToAction("Events");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Models.XMLEvents events = new Models.XMLEvents();
                foreach (Models.XMLEvent eve in events.eventList)
                {
                    if (eve.id == id)
                    {
                        return View(eve);
                    }
                }
                return RedirectToAction("Events");
            }
            catch
            {
                return RedirectToAction("Events");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Models.XMLEvents events = new Models.XMLEvents();
                foreach (Models.XMLEvent eve in events.eventList)
                {
                    if (eve.id == id)
                    {
                        //eve.eventIdentifier = collection["eventIdentifier"];
                        eve.eventName = collection["eventName"];
                        eve.date = collection["date"];
                        eve.time = collection["time"];
                        eve.location = collection["location"];
                        eve.description = collection["description"];
                        events.Save();
                        break;
                    }
                }
                return RedirectToAction("Events");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Models.XMLEvents events = new Models.XMLEvents();
                foreach (Models.XMLEvent eve in events.eventList)
                {
                    if (eve.id == id)
                    {
                        return View(eve);
                    }
                }
                return RedirectToAction("Events");
            }
            catch
            {
                return RedirectToAction("Events");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Models.XMLEvents events = new Models.XMLEvents();
                foreach (Models.XMLEvent eve in events.eventList)
                {
                    if (eve.id == id)
                    {
                        events.eventList.Remove(eve);
                        events.Save();
                        break;
                    }
                }
                return RedirectToAction("Events");
            }
            catch
            {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
	}
}