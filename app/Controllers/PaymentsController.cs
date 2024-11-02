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
                    var data = db.vw_employee_payroll.OrderByDescending(f => f.created_date).ToList();
                    return Json(new { status = true, message = "", data });
                }
                else
                {
                    var data = db.vw_employee_payroll.Where(f => f.payroll_id == Guid.Parse(id)).FirstOrDefault();
                    return Json(new { status = true, message = "", data });
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = true, message = ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.InnerException, data = new List<employees>() });
            }
        }

        [HttpPost]
        public JsonResult AddData(payrolls data)
        {
            try
            {
                data.created_date = DateTime.Now;
                if (data.created_date.HasValue)
                {
                    data.created_date = DateTime.SpecifyKind(data.created_date.Value, DateTimeKind.Utc);
                }
                if (data.update_date.HasValue)
                {
                    data.update_date = DateTime.SpecifyKind(data.update_date.Value, DateTimeKind.Utc);
                }
                if (data.deleted_date.HasValue)
                {
                    data.deleted_date = DateTime.SpecifyKind(data.deleted_date.Value, DateTimeKind.Utc);
                }
                // If the period is not already in UTC, specify it
                data.period = DateTime.SpecifyKind(data.period, DateTimeKind.Utc);

                db.payrolls.Add(data);
                db.SaveChanges();
                return Json(new { status = true, message = "Payroll added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.InnerException });
            }
        }

        [HttpPost]
        public JsonResult UpdateData(payrolls data)
        {
            try
            {
                data.update_date = DateTime.Now;
                if (data.created_date.HasValue)
                {
                    data.created_date = DateTime.SpecifyKind(data.created_date.Value, DateTimeKind.Utc);
                }
                if (data.update_date.HasValue)
                {
                    data.update_date = DateTime.SpecifyKind(data.update_date.Value, DateTimeKind.Utc);
                }
                if (data.deleted_date.HasValue)
                {
                    data.deleted_date = DateTime.SpecifyKind(data.deleted_date.Value, DateTimeKind.Utc);
                }
                // If the period is not already in UTC, specify it
                data.period = DateTime.SpecifyKind(data.period, DateTimeKind.Utc);
                db.payrolls.Update(data);
                db.SaveChanges();
                return Json(new { status = true, message = "Payroll added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.InnerException });
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
                return Json(new { status = false, message = ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.InnerException });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}