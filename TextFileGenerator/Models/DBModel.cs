using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextFileGenerator.Models
{
    [Table("Records")]
    public class DBModel    //Модель табоицы Record в базе данных
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "String contain not 10 characters")]
        public string Latine { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "String contain not 10 characters")]
        public string Cirilic { get; set; }

        [Required]
        [Range(1, 100_000_000, ErrorMessage = "Incorect integer number")]
        public int Integer { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Incorect real number")]
        public double Real { get; set; }
    }
}
