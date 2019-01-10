using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using StudentTest.Models;

namespace StudentTest.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {

            List<Student> result = TempData["Result"] as List<Student>;
            ViewBag.status = TempData["Status"] as string;

            return View(result);
        }

        [HttpPost]
        public ActionResult ShowAll(FormCollection post) {

            using (var ctx = new StudentTestEntities()) {
                List<Student> result = new List<Student>();
                result = ctx.Student.ToList();
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(FormCollection post) {

            String InputInfo = post["InputInfo"].ToString();
            String SearchCol = post["SearchCol"].ToString();

            using (var ctx = new StudentTestEntities()) {
                List<Student> result = new List<Student>();
                if (SearchCol == "StudentNo") {
                    result = (from s in ctx.Student where s.StudentNo == InputInfo select s).ToList();
                } else if (SearchCol == "StudentName") {
                    result = (from s in ctx.Student where s.StudentName == InputInfo select s).ToList();
                } else if (SearchCol == "Birthday") {
                    result = (from s in ctx.Student where s.Birthday == InputInfo select s).ToList();
                } else if (SearchCol == "Department") {
                    result = (from s in ctx.Student where s.Department == InputInfo select s).ToList();
                }
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddData(FormCollection post) {

            string StudentNo = post["StudentNo"];
            string StudentName = post["StudentName"];
            string Birthday = post["Birthday"];
            string Department = post["Department"];

            if (StudentNo != "" && StudentName != "" && Birthday != "" && Department != "") {
                using (var ctx = new StudentTestEntities()) {
                    Student stu = ctx.Student.Find(StudentNo);
                    if (stu == null) {
                        stu = new Student() { StudentNo = StudentNo, StudentName = StudentName, Birthday = Birthday, Department = Department };
                        ctx.Student.Add(stu);
                        try {
                            ctx.SaveChanges();
                        } catch {
                            TempData["Status"] = "資料格式錯誤";
                        }
                    } else {
                        TempData["Status"] = StudentNo + " 已經存在";
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}