using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
     public class MyCloth
    {
         public IEnumerable<MvcApplication1.Models.cloth> MyCloth1 { get; set; }
         public MyCloth()
        {
            forecastflowEntities1 db = new forecastflowEntities1();
            this.MyCloth1 = db.cloth.ToList();
        }
         public MyCloth(int id)
        {
            forecastflowEntities1 db = new forecastflowEntities1();
            this.MyCloth1 = db.cloth.ToList().Where(t => t.clothid == id);
        }
    }
}