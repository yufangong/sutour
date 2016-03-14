using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final1.Controllers
{
     
    public class HomeController : Controller
    {
        public Models.ImageContext db = new Models.ImageContext();
        public Models.Image[] getImagesWithAnnotation(int count)
        {
            var imagesannotation = new HashSet<Models.Image>();
            List<int> landID = new List<int>();
            Random random = new Random();
            if (count > db.Images.ToList().Count())
            {
                throw new ArgumentOutOfRangeException("count");
            }

            while (imagesannotation.Count < count)
            {
                //using an hashset will ensure a random set with unique values.
                //Image image = db.Images.Find(random.Next(db.Images.ToList().Count));
                var existImage = db.Images.Find(random.Next(1, db.Images.ToList().Count));
                if (existImage != null && !landID.Contains(existImage.landmarks.LandmarkId))
                {
                    imagesannotation.Add(existImage);
                    landID.Add(existImage.landmarks.LandmarkId);
                }
            }
            return imagesannotation.ToArray();
        }
        public Models.CityImage[] getCityImages(int count)
        {
            var cityimage = new HashSet<Models.CityImage>();
            Random random = new Random();
            if (count > db.CityImages.ToList().Count())
            {
                throw new ArgumentOutOfRangeException("count");
            }

            while (cityimage.Count < count)
            {
                //using an hashset will ensure a random set with unique values.
                //Image image = db.Images.Find(random.Next(db.Images.ToList().Count));
                var existImage = db.CityImages.Find(random.Next(1, db.Images.ToList().Count));
                if (existImage != null)
                {
                    cityimage.Add(existImage);
                }
            }
            return cityimage.ToArray();
        }
        public ActionResult Index()
        {
            //string[] images = Models.RandomImage.GetImages("pic", 3);
            Models.Image[] images2 = getImagesWithAnnotation(3);
            Models.CityImage[] dbcityimage = getCityImages(3);
            string[] cityimage = new string[3];
            string[] cityannotation = new string[3];

            string[] campusImage = new string[3];
            string[] annotation = new string[3];
            int[] imageId = new int[3];
            int[] cityId = new int[3];

            cityimage[0] = dbcityimage[0].Name.ToString();
            cityimage[1] = dbcityimage[1].Name.ToString();
            cityimage[2] = dbcityimage[2].Name.ToString();

            cityannotation[0] = dbcityimage[0].Annotation;
            cityannotation[1] = dbcityimage[1].Annotation;
            cityannotation[2] = dbcityimage[2].Annotation;


            campusImage[0] = images2[0].Name.ToString();
            campusImage[1] = images2[1].Name.ToString();
            campusImage[2] = images2[2].Name.ToString();

            annotation[0] = images2[0].Annotation;
            annotation[1] = images2[1].Annotation;
            annotation[2] = images2[2].Annotation;

            cityId[0] = dbcityimage[0].ID;
            cityId[1] = dbcityimage[1].ID;
            cityId[2] = dbcityimage[2].ID;

            imageId[0] = images2[0].ImageId;
            imageId[1] = images2[1].ImageId;
            imageId[2] = images2[2].ImageId;

            ViewBag.CityImage = cityimage;
            ViewBag.Cityannotation = cityannotation;
            ViewBag.CityID = cityId;

            //ViewBag.CampusImages = images;
            ViewBag.dbimage = campusImage;
            ViewBag.dbannotation = annotation;
            ViewBag.dbid = imageId;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Resources()
        {
            ViewBag.Message = "Resource Pages.";

            return View();
        }
    }
}