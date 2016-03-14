using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Caching;
using System.ComponentModel.DataAnnotations;

namespace final1.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Annotation { get; set; }
        public string UploadTime { get; set; }
        public string Uploader { get; set; }
        public virtual Landmark landmarks { get; set; }
    }
    public class CityImage
    {
        public int ID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Annotation { get; set; }
        public string UploadTime { get; set; }
        public string Uploader { get; set; }
    }

    //public class RandomImage
    //{
    //    public static string[] GetImages(string folder, int count)
    //    {
    //        HttpContext context = HttpContext.Current;
    //        string virtualFolderPath = string.Format("/content/{0}/", folder);
    //        string absoluteFolderPath = context.Server.MapPath(virtualFolderPath);

    //        Cache cache = context.Cache;
    //        var images = cache[folder + "_images"] as List<string>;

    //        // cache string array if it does not exist
    //        if (images == null)
    //        {
    //            var di = new DirectoryInfo(absoluteFolderPath);
    //            images = (from fi in di.GetFiles()
    //                      where fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".gif"
    //                      select string.Format("{0}{1}", virtualFolderPath, fi.Name))
    //                            .ToList();


    //            // create cach dependency on image randomFolderName
    //            cache.Insert(folder + "_images", images, new CacheDependency(absoluteFolderPath));
    //        }

    //        Random random = new Random();
    //        var imageSet = new HashSet<string>();
    //        if (count > images.Count())
    //        {
    //            throw new ArgumentOutOfRangeException("count");
    //        }

    //        while (imageSet.Count() < count)
    //        {
    //            //using an hashset will ensure a random set with unique values.
    //            imageSet.Add(images[random.Next(images.Count())]);
    //        }

    //        return imageSet.ToArray();
    //    }
    //}

     
}