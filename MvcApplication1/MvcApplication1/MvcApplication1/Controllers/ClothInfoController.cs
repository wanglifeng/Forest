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
        public ActionResult Add([Bind(Exclude = "Id")] MvcApplication1.Models.cloth data)
        {
            _db.AddTocloth(data);

            _db.SaveChanges();

            return new JsonResult() { Data = true };

        }
       
        public ActionResult Index()
        {
   
            return View();
        }

    }
}
