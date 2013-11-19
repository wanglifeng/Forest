using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /List/
        private forecastflowEntities1 _db = new forecastflowEntities1();

        public ActionResult Index(int pageid)
        {
            //return View(_db.data);
            //int Pageid = 1;
            int flag = pageid;
            jisuan(flag);
            if(pageid==1)
            {
                ViewBag.m = "右臂";
            }
            else if (pageid == 2)
            {
                ViewBag.m = "前胸";
            }else if (pageid == 3)
            {
                ViewBag.m = "后背";
            }
            else if (pageid == 4)
            {
                ViewBag.m = "左臂";
            }
            
            
            // return View(_db.data.Where(t => t.flag == flag));

            Indexdata ind = new Indexdata(flag);
            return View(ind);
        }
        //public ActionResult Index()
        //{
        //    return View(_db.data);
        //}


        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Add([Bind(Exclude = "Id")] MvcApplication1.Models.data data)
        {
            _db.AddTodata(data);

            _db.SaveChanges();

            return new JsonResult() { Data = true };

        }
       
        public ActionResult forcast(int pageid,double cair)
        {
            var a = 1;
            return RedirectToAction("Index", new { pageid = pageid });
        }

        public ActionResult Delete(int id,int pageid)
        {
            var rec = _db.data.SingleOrDefault(t => t.id == id);

            _db.data.DeleteObject(rec);
            _db.SaveChanges();

            //actually, you hvae deleted the record
            //if you want to redirect to a url, you need use redirect funciton

            //return Redirect("List?pageid="+id);
            return RedirectToAction("Index", new { pageid = pageid });
            //return RedirectToAction("List?pageid="+id);
            // return new JsonResult() { Data = true };
        }

        public void jisuan(int flag)
        {
            double pingjun_in = 0;
            double pingjun_out = 0;
            double pingjun_flow = 0;
            double cair=0;
            double vent = 0;
            int count = 0;
            double flowhe = 0;
            var rec = _db.data.Where(t => t.flag == flag);
            foreach (var item in rec)
            {
                count++;
                if (item.flow == 0)
                {
                    item.flow = 1;
                }
                pingjun_in += (double)(item.fin) * (double)(item.flow);
                pingjun_out += (double)(item.fout) * (double)(item.flow);
                flowhe += (double)(item.flow);
            }

            if (flowhe == 0 || count == 0)
            {
                flowhe = 1;
                count = 1;
            }

            pingjun_in = pingjun_in / flowhe;
            pingjun_out = pingjun_out / flowhe;
            pingjun_flow = flowhe / count;
            if (pingjun_out == 0)
            {
                pingjun_out = 1;
            }
            vent = ((pingjun_in - pingjun_out) / (pingjun_out - cair)) * pingjun_flow;

            var rec1 = _db.ave.SingleOrDefault(t => t.flag == flag);
            if (rec1 != null)
            {
                _db.ave.DeleteObject(rec1);
            }
            _db.SaveChanges();
            int a = MyClass.myclothcount;
            MvcApplication1.Models.ave avr = new MvcApplication1.Models.ave
            {
                cin = (decimal)pingjun_in,
                cout = (decimal)pingjun_out,
                cflow = (decimal)pingjun_flow,
                flag = flag,
                name = _db.dic.Single(t => t.flag == flag).name
            };

            MvcApplication1.Models.ave average = new MvcApplication1.Models.ave();
            MvcApplication1.Models.Myave average1 = new MvcApplication1.Models.Myave();

            _db.AddToave(avr);
            _db.SaveChanges();

            //average.cflow = pingjun_flow;
            //average.cin = pingjun_in;
            //average.cout = pingjun_out;
            average.flag = flag;

            _db.SaveChanges();

        }

    }
}
