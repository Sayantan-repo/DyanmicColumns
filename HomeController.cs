using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using WebAppDemo.Utility;
using WebAppDemo.ViewModels;

namespace WebAppDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult StudentView()
        {
            string pageName = "StudentView";
            List<string> cols = DataAccessHelper.GetAllIncludedColumns(pageName);
            ViewBag.ColList = cols;
            return View();
        }


        public JsonResult GetAllStudents(jQueryDataTableParamModel param)
        {
            string pageName = "StudentView";
            List<List<string>> result = new List<List<string>>();
            List<StudentViewModel> StudentList = DataAccessHelper.GetAllStudents();
            int RecordCount = StudentList.Count;
            string[] columnNames = DataAccessHelper.GetDynamicColumns(pageName).Split(',').ToArray();

            foreach (var item in StudentList)
            {
                List<string> val = new List<string>();
                foreach (var prop in columnNames)
                {
                    if (prop == "Id")
                    {
                        val.Add(Convert.ToString(item.Id));
                    }
                    if (prop == "StudentName")
                    {
                        val.Add(string.IsNullOrEmpty(item.Name) ? "" : item.Name);
                    }
                    else if (prop == "Email")
                    {
                        val.Add(string.IsNullOrEmpty(item.Email) ? "" : item.Email);
                    }
                    else if (prop == "Contact")
                    {
                        val.Add(string.IsNullOrEmpty(item.ContactNumber) ? "" : item.ContactNumber);
                    }
                    else if (prop == "Gender")
                    {
                        val.Add(string.IsNullOrEmpty(item.Gender) ? "" : item.Gender);
                    }
                    else if (prop == "Country")
                    {
                        val.Add(string.IsNullOrEmpty(item.Country) ? "" : item.Country);
                    }
                    else if (prop == "City")
                    {
                        val.Add(string.IsNullOrEmpty(item.City) ? "" : item.City);
                    }
                    else if (prop == "State")
                    {
                        val.Add(string.IsNullOrEmpty(item.State) ? "" : item.State);
                    }
                    else if (prop == "Gender")
                    {
                        val.Add(string.IsNullOrEmpty(item.Gender) ? "" : item.Gender);
                    }
                    else if (prop == "BloodGroup")
                    {
                        val.Add(string.IsNullOrEmpty(item.BloodGroup) ? "" : item.BloodGroup);
                    }
                    else if (prop == "Zip")
                    {
                        val.Add(string.IsNullOrEmpty(item.Zip) ? "" : item.Zip);
                    }
                }

                result.Add(val);
            }

            return Json(new
            {
                paginationDisplay = 10,
                sEcho = param.sEcho,
                iTotalRecords = RecordCount,
                iTotalDisplayRecords = RecordCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllColumnsPageWise(string PageName)
        {
            List<AllColumns> lstCol = new List<AllColumns>();
            lstCol = DataAccessHelper.GetAllColumns(PageName);
            return Json(lstCol, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveColumns(string InclCol, string ExclCol, string PageName)
        {
            string[] strCustomColIncl = InclCol.Split(',');
            string[] strCustomColExcl = ExclCol.Split(',');

            foreach (var item in strCustomColIncl)
            {
                DataAccessHelper.UpdateInculdedColumns(Convert.ToInt16(item), PageName);
            }

            if (!string.IsNullOrEmpty(strCustomColExcl[0]))
            {
                foreach (var item1 in strCustomColExcl)
                {
                    DataAccessHelper.UpdateExcludedColumns(Convert.ToInt16(item1), PageName);
                }
            }


            List<string> cols = DataAccessHelper.GetAllIncludedColumns(PageName);
            ViewBag.ColList = cols;
            return Json("");
        }
    }
}