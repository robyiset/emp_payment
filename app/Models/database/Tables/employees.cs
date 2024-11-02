using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models.database.Tables
{
    [Table("employees")]
    public class employees
    {
        [Key]
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public Guid? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public Guid? update_by { get; set; }
        public DateTime? update_date { get; set; }
        public Guid? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
    }
}