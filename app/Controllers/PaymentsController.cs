using app.Models.database;
using app.Models.database.Tables;
using Microsoft.AspNetCore.Mvc;

namespace emp_payment.Controllers
{
    public class PaymentsController : Controller
    {
        private EmpPaymentContext db;

        public PaymentsController(EmpPaymentContext sdb)
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
                    var data = db.payrolls.OrderByDescending(f => f.created_date).ToList();
                    return Json(new { status = true, message = "", data });
                }
                else
                {
                    var data = db.payrolls.Where(f => f.id == Guid.Parse(id)).FirstOrDefault();
                    return Json(new { status = true, message = "", data });
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = true, message = ex.Message, data = new List<employees>() });
            }
        }

        [HttpPost]
        public JsonResult AddData(payrolls data)
        {
            try
            {
                db.payrolls.Add(data);
                db.SaveChanges();
                return Json(new { status = true, message = "Payroll added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateData(payrolls data)
        {
            try
            {
                db.payrolls.Update(data);
                db.SaveChanges();
                return Json(new { status = true, message = "Payroll added successfully" });
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
                var payroll = db.payrolls.FirstOrDefault(e => e.id == Guid.Parse(id));
                if (payroll == null)
                {
                    return Json(new { status = false, message = "Payroll not found" });
                }

                db.payrolls.Remove(payroll);
                db.SaveChanges();

                return Json(new { status = true, message = "Payroll deleted successfully" });
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