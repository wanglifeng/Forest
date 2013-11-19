using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{

    public class ClothInfoController : Controller
    {
        //
        private forecastflowEntities1 _db = new forecastflowEntities1();
        // GET: /ClothInfo/
        [HttpPost]
        public ActionResult Add( MvcApplication1.Models.cloth data)
        {
            data.clothid = _db.cloth.Count() + 1;
            _db.AddTocloth(data);

            _db.SaveChanges();

            return new JsonResult() { Data = true };

        }
       
        public ActionResult Index()
        {
            //MyClass.myclothcount = _db.cloth.Count();
            //_db.cloth.ToList().Where(t => t.clothid == MyClass.myclothcount);
            Indexdata ind = new Indexdata();
            return View(ind);
            //MyCloth cl = new MyCloth(2);
            //return View(cl);
        }

    }
}
