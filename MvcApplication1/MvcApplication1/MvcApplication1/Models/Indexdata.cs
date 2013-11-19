using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Indexdata
    {
        public IEnumerable<MvcApplication1.Models.data> data { get; set; }
        public IEnumerable<MvcApplication1.Models.ave> ave { get; set; }
         public IEnumerable<MvcApplication1.Models.cloth> cloth { get; set; }
       
        public Indexdata(int flag)
        {
            forecastflowEntities1 db = new forecastflowEntities1();
            this.data = db.data.ToList().Where(t => t.flag == flag);
            this.ave = db.ave.ToList().Where(t => t.flag == flag);
            this.cloth = db.cloth.ToList().Where(t => t.clothid == 2);
        }
        public Indexdata()
        {
            forecastflowEntities1 db = new forecastflowEntities1();
            this.data = db.data.ToList();
            this.ave = db.ave.ToList();
            this.cloth = db.cloth.ToList();
        }
    }
}