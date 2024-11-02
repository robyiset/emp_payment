using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models.database.Tables
{
    [Table("payrolls")]
    public class payrolls
    {
        [Key]
        public Guid id { get; set; }
        public Guid? employee_id { get; set; }
        public DateTime period { get; set; }
        public decimal basic_salary { get; set; }
        public decimal? bonus { get; set; }
        public decimal? deductions { get; set; }
        public decimal total_salary { get; set; }
        public Guid? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public Guid? update_by { get; set; }
        public DateTime? update_date { get; set; }
        public Guid? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
    }
}