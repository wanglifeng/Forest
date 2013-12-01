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
            if (MyClass.ifadd == 1)
            {
                MyClass.myclothcount = _db.cloth.Max(s => s.clothid);
                MyClass.myclothcount=MyClass.myclothcount + 1;
                data.clothid = MyClass.myclothcount;
                _db.AddTocloth(data);
                MyClass.ifadd = 0;
                _db.SaveChanges();
            }
            else
            {
                data.clothid = MyClass.myclothcount;
                var rec1 = _db.cloth.SingleOrDefault(t => t.clothid == data.clothid);
                if (rec1 != null)
                {
                    _db.cloth.DeleteObject(rec1);
                }
                data.clothid = MyClass.myclothcount;
                _db.AddTocloth(data);
                _db.SaveChanges();
            }
       

            return new JsonResult() { Data = true };

        }
       
        public ActionResult Index()
        {
            if (MyClass.myclothcount == 0)
            {
                MyClass.ifadd = 1;
            }
            
            //_db.cloth.ToList().Where(t => t.clothid == MyClass.myclothcount);
            Indexdata ind = new Indexdata(MyClass.myclothcount);

            if (MyClass.ifadd == 1)
            {
                foreach (var item in ind.cloth)
                {
                    item.chengfen = "";
                    item.houdu = "";
                    item.touqixing = "";
                    item.xuanchuixing = "";
                    item.yiling = "";
                    item.xiukou = "";
                    item.dibai = "";
                    item.qitakaikou = "";
                    item.haoxing = "";
                    item.lingwei = "";
                    item.xiukouwei = "";
                    item.xiabaiwei = "";
                    item.qita = "";
                }
                return View(ind);
            }
            else
            {
                MyClass.myclothcount = _db.cloth.Max(s => s.clothid);
            }
            
            return View(ind);
            //MyCloth cl = new MyCloth(2);
            //return View(cl);
        }

    }
}
