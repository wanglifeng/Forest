using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ForecastController : Controller
    {
        //
        // GET: /Forecast/
        private forecastflowEntities1 _db = new forecastflowEntities1();
        public double vent;
        public double cair;
       // public double ViewData["data"] = colors;
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    //ViewBag.Message = "Welcome to ASP.NET MVC!";
        //    Indexdata ind = new Indexdata();
        //    ViewBag.mess = cair;
        //    jisuan(cair);
        //    return View(ind);
        //}
        public ActionResult Index(double cair)
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            jisuan(cair);
            Indexdata ind = new Indexdata();
            ViewBag.mess = cair;
            
            return View(ind);
        }
        public ActionResult forcast( double cair)
        {
            var a = 1;
            return RedirectToAction("Index", new { cair = cair });
        }

        public void jisuan(double cair)
        {
            double pingjun_in = 0;
            double pingjun_out = 0;
            double pingjun_flow = 0;
            //double cair = 0;
            //double vent = 0;
            int count = 0;
            double jubu_vent = 0;
            double flowhe = 0;
            var rec = _db.ave;
            foreach (var item in rec)
            {
                count++;
                if (item.cflow == 0)
                {
                    item.cflow = 1;
                }
                pingjun_in += (double)(item.cin) * (double)(item.cflow);
                pingjun_out += (double)(item.cout) * (double)(item.cflow);
                flowhe += (double)(item.cflow);

                item.tongfeng = (decimal)(((item.cin - item.cout) / (item.cout - (decimal)cair)) * (item.cflow));
            }
            _db.SaveChanges();
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
            ViewBag.Message = vent;
            //var rec1 = _db.ave.SingleOrDefault(t => t.flag == flag);
            //if (rec1 != null)
            //{
            //    _db.ave.DeleteObject(rec1);
            //}
            //_db.SaveChanges();
            //MvcApplication1.Models.ave avr = new MvcApplication1.Models.ave
            //{
            //    cin = (decimal)pingjun_in,
            //    cout = (decimal)pingjun_out,
            //    cflow = (decimal)pingjun_flow,
            //    flag = flag,
            //    name = _db.dic.Single(t => t.flag == flag).name
            //};

            //MvcApplication1.Models.ave average = new MvcApplication1.Models.ave();
            //MvcApplication1.Models.Myave average1 = new MvcApplication1.Models.Myave();

            //_db.AddToave(avr);
            //_db.SaveChanges();

            ////average.cflow = pingjun_flow;
            ////average.cin = pingjun_in;
            ////average.cout = pingjun_out;
            //average.flag = flag;

            //_db.SaveChanges();

        }

    }
}
