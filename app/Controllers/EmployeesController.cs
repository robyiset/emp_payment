using app.Models.database;
using app.Models.database.Tables;
using Microsoft.AspNetCore.Mvc;

namespace emp_payment.Controllers
{
    public class EmployeesController : Controller
    {
        private EmpPaymentContext db;

        public EmployeesController(EmpPaymentContext sdb)
        {
            db = sdb;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetData(string? id)
        {
            try
            {
                if (id == null)
                {
                    var data = db.employees.OrderByDescending(f => f.created_date).ToList();
                    return Json(new { status = true, message = "", data });
                }
                else
                {
                    var data = db.employees.Where(f => f.id == Guid.Parse(id)).FirstOrDefault();
                    return Json(new { status = true, message = "", data });
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = true, message = ex.Message, data = new List<employees>() });
            }
        }

        [HttpPost]
        public JsonResult AddData(employees data)
        {
            try
            {
                db.employees.Add(data);
                db.SaveChanges();
                return Json(new { status = true, message = "Employee added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateData(employees data)
        {
            try
            {
                db.employees.Update(data);
                db.SaveChanges();
                return Json(new { status = true, message = "Employee added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteData(string id)
        {
            try
            {
                var employee = db.employees.FirstOrDefault(e => e.id == Guid.Parse(id));
                if (employee == null)
                {
                    return Json(new { status = false, message = "Employee not found" });
                }

                db.employees.Remove(employee);
                db.SaveChanges();

                return Json(new { status = true, message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}