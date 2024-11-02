using System.ComponentModel.DataAnnotations.Schema;
namespace app.Models.database.Views
{
    [Table("vw_employee_payroll")]
    public class vw_employee_payroll
    {
        public Guid? payroll_id { get; set; }
        public Guid? employee_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? position { get; set; }
        public string? department { get; set; }
        public DateTime? period { get; set; }
        public decimal? basic_salary { get; set; }
        public decimal? bonus { get; set; }
        public decimal? deductions { get; set; }
        public decimal? total_salary { get; set; }
        public DateTime? created_date { get; set; }
    }
}