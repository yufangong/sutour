using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace final1.Models
{
    public class ImageContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ImageContext() : base("name=ImageContext")
        {
        }

        public System.Data.Entity.DbSet<final1.Models.Image> Images { get; set; }
        public System.Data.Entity.DbSet<final1.Models.CityImage> CityImages { get; set; }

        public System.Data.Entity.DbSet<final1.Models.Landmark> Landmarks { get; set; }

        public System.Data.Entity.DbSet<final1.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<final1.Models.XMLEvent> XMLEvents { get; set; }

    
    }
}
