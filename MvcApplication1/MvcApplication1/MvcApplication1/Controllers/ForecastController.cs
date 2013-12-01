using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Data;

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
            jisuan(cair);
            //export(cair);
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
            //_db.SaveChanges();
            if (flowhe == 0 || count == 0)
            {
                flowhe = 1;
                count = 1;
                pingjun_flow = 0;
            }
            else
            {

                pingjun_flow = flowhe / count;
            }

            pingjun_in = pingjun_in / flowhe;
            pingjun_out = pingjun_out / flowhe;
            //if (flowhe == 0 || count == 0)
            //{
            //    flowhe = 1;
            //    count = 1;
            //}

            //pingjun_in = pingjun_in / flowhe;
            //pingjun_out = pingjun_out / flowhe;
            //pingjun_flow = flowhe / count;
            //if (pingjun_out == 0)
            //{
            //    pingjun_out = 1;
            //}
            //vent = ((pingjun_in - pingjun_out) / (pingjun_out - cair)) * pingjun_flow;
            if ((pingjun_out - cair) == 0)
            {
                vent = 0;
            }
            else
            {
                vent = ((pingjun_in - pingjun_out) / (pingjun_out - cair)) * pingjun_flow;
            }
            ViewBag.Message = vent;

            //if (pingjun_out == 1)
            //{
            //    pingjun_out = 0;
            //}
            //if (pingjun_flow == 1)
            //{
            //    pingjun_flow = 0;
            //}
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


        protected void ExportExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;
        }

        public ActionResult export(double cair)
        {
            jisuan(cair);

            if (MyClass.myclothcount == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("服装信息", typeof(string));
                dt.Columns.Add("属性值", typeof(string));
                dt.Columns.Add("部位", typeof(string));
                dt.Columns.Add("通风值（%）", typeof(string));
                dt.Columns.Add("气体浓度(L/min)", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));

                DataRow dr = dt.NewRow();
                //dr["cloth"] = MyClass.myclothcount;
                //var rec1 = _db.cloth.SingleOrDefault(t => t.clothid == MyClass.myclothcount);
                //dr["成分"] = rec1.chengfen;
                //dr["厚度"] = rec1.houdu;
                //dr["透气性"] = rec1.touqixing;
                //dr["悬垂性"] = rec1.xuanchuixing;
                //dr["衣领"] = rec1.yiling;
                //dr["袖口"] = rec1.xiukou;
                //dr["底摆"] = rec1.dibai;
                //dr["其它开口部位"] = rec1.qitakaikou;
                //dr["号型"] = rec1.haoxing;
                //dr["领围"] = rec1.lingwei;
                //dr["袖口围"] = rec1.xiukouwei;
                //dr["下摆围"] = rec1.xiabaiwei;
                //dr["其他"] = rec1.qita;
                dt.Rows.Add(dr);

                for (int i = 0; i < 20; i++)
                {
                    DataRow dr1 = dt.NewRow();
                    dt.Rows.Add(dr1);
                }
                dt.Rows[0][0] = "cloth";
                dt.Rows[1][0] = "成分";
                dt.Rows[2][0] = "厚度";
                dt.Rows[3][0] = "透气性";
                dt.Rows[4][0] = "悬垂性";
                dt.Rows[5][0] = "衣领";
                dt.Rows[6][0] = "袖口";
                dt.Rows[7][0] = "底摆";
                dt.Rows[8][0] = "其它开口部位";
                dt.Rows[9][0] = "号型";
                dt.Rows[10][0] = "领围";
                dt.Rows[11][0] = "袖口围";
                dt.Rows[12][0] = "下摆围";
                dt.Rows[13][0] = "其他";

                dt.Rows[0][1] = MyClass.myclothcount;
                var rec1 = _db.cloth.SingleOrDefault(t => t.clothid == MyClass.myclothcount);
                dt.Rows[1][1] = "";
                dt.Rows[2][1] = "";
                dt.Rows[3][1] = "";
                dt.Rows[4][1] = "";
                dt.Rows[5][1] = "";
                dt.Rows[6][1] = "";
                dt.Rows[7][1] = "";
                dt.Rows[8][1] = "";
                dt.Rows[9][1] = "";
                dt.Rows[10][1] = "";
                dt.Rows[11][1] = "";
                dt.Rows[12][1] = "";
                dt.Rows[13][1] = "";
                //var query = from p in _db.ave
                //        select new { clothid = MyClass.myclothcount, flag = '4' };
                var q = from p in _db.ave where p.clothid == MyClass.myclothcount && p.flag == '4' select p.tongfeng;
                var rec = _db.ave.SingleOrDefault(t => t.flag == 1);
                var rec2 = _db.ave.SingleOrDefault(t => t.flag == 2);
                var rec3 = _db.ave.SingleOrDefault(t => t.flag == 3);
                var rec4 = _db.ave.SingleOrDefault(t => t.flag == 4);
                dt.Rows[0][2] = "整体";
                dt.Rows[0][3] = vent;//String.Format("{0:F}", vent);
                dt.Rows[0][4] = cair;
                //dt.Rows[1][2] = "服装部位";
                //dt.Rows[1][3] = "局部通风值";

                dt.Rows[1][2] = "前胸";
                dt.Rows[2][2] = "后背";
                dt.Rows[3][2] = "右臂";
                dt.Rows[4][2] = "左臂";

                dt.Rows[1][3] = "";
                dt.Rows[2][3] = "";
                dt.Rows[3][3] = "";
                dt.Rows[4][3] = "";

                dt.AcceptChanges();
                ExportExcel(dt);
            }
            else
            {
                MyClass.myclothcount = _db.cloth.Max(s => s.clothid);
                DataTable dt = new DataTable();
                dt.Columns.Add("服装信息", typeof(string));
                dt.Columns.Add("属性值", typeof(string));
                dt.Columns.Add("部位", typeof(string));
                dt.Columns.Add("通风值（%）", typeof(string));
                dt.Columns.Add("气体浓度(L/min)", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));
                //dt.Columns.Add("", typeof(string));

                DataRow dr = dt.NewRow();
                //dr["cloth"] = MyClass.myclothcount;
                //var rec1 = _db.cloth.SingleOrDefault(t => t.clothid == MyClass.myclothcount);
                //dr["成分"] = rec1.chengfen;
                //dr["厚度"] = rec1.houdu;
                //dr["透气性"] = rec1.touqixing;
                //dr["悬垂性"] = rec1.xuanchuixing;
                //dr["衣领"] = rec1.yiling;
                //dr["袖口"] = rec1.xiukou;
                //dr["底摆"] = rec1.dibai;
                //dr["其它开口部位"] = rec1.qitakaikou;
                //dr["号型"] = rec1.haoxing;
                //dr["领围"] = rec1.lingwei;
                //dr["袖口围"] = rec1.xiukouwei;
                //dr["下摆围"] = rec1.xiabaiwei;
                //dr["其他"] = rec1.qita;
                dt.Rows.Add(dr);

                for (int i = 0; i < 20; i++)
                {
                    DataRow dr1 = dt.NewRow();
                    dt.Rows.Add(dr1);
                }
                dt.Rows[0][0] = "cloth";
                dt.Rows[1][0] = "成分";
                dt.Rows[2][0] = "厚度";
                dt.Rows[3][0] = "透气性";
                dt.Rows[4][0] = "悬垂性";
                dt.Rows[5][0] = "衣领";
                dt.Rows[6][0] = "袖口";
                dt.Rows[7][0] = "底摆";
                dt.Rows[8][0] = "其它开口部位";
                dt.Rows[9][0] = "号型";
                dt.Rows[10][0] = "领围";
                dt.Rows[11][0] = "袖口围";
                dt.Rows[12][0] = "下摆围";
                dt.Rows[13][0] = "其他";

                dt.Rows[0][1] = MyClass.myclothcount;
                var rec1 = _db.cloth.SingleOrDefault(t => t.clothid == MyClass.myclothcount);
                dt.Rows[1][1] = rec1.chengfen;
                dt.Rows[2][1] = rec1.houdu;
                dt.Rows[3][1] = rec1.touqixing;
                dt.Rows[4][1] = rec1.xuanchuixing;
                dt.Rows[5][1] = rec1.yiling;
                dt.Rows[6][1] = rec1.xiukou;
                dt.Rows[7][1] = rec1.dibai;
                dt.Rows[8][1] = rec1.qitakaikou;
                dt.Rows[9][1] = rec1.haoxing;
                dt.Rows[10][1] = rec1.lingwei;
                dt.Rows[11][1] = rec1.xiukouwei;
                dt.Rows[12][1] = rec1.xiabaiwei;
                dt.Rows[13][1] = rec1.qita;
                //var query = from p in _db.ave
                //        select new { clothid = MyClass.myclothcount, flag = '4' };
                var q = from p in _db.ave where p.clothid == MyClass.myclothcount && p.flag == '4' select p.tongfeng;
                var rec = _db.ave.SingleOrDefault(t => t.flag == 1);
                var rec2 = _db.ave.SingleOrDefault(t => t.flag == 2);
                var rec3 = _db.ave.SingleOrDefault(t => t.flag == 3);
                var rec4 = _db.ave.SingleOrDefault(t => t.flag == 4);
                dt.Rows[0][2] = "整体";
                dt.Rows[0][3] = vent;//String.Format("{0:F}", vent);
                dt.Rows[0][4] = cair;
                //dt.Rows[1][2] = "服装部位";
                //dt.Rows[1][3] = "局部通风值";

                dt.Rows[1][2] = "前胸";
                dt.Rows[2][2] = "后背";
                dt.Rows[3][2] = "右臂";
                dt.Rows[4][2] = "左臂";

                dt.Rows[1][3] = rec.tongfeng;
                dt.Rows[2][3] = rec2.tongfeng;
                dt.Rows[3][3] = rec3.tongfeng;
                dt.Rows[4][3] = rec4.tongfeng;

                dt.AcceptChanges();
                ExportExcel(dt);
           
                MyClass.myclothcount = 0;
            }
            return RedirectToAction("Index", new { cair = cair });
        }
    }
}
