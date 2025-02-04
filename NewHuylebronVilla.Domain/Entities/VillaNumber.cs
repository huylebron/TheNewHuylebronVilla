using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHuylebronVilla.Domain.Entities
{
    public class VillaNumber
    {
        [Key,
          DatabaseGenerated(DatabaseGeneratedOption.None)] // ghi đè lên mặc định của EF core   để không tự tăng 
        public int Villa_Number { get; set; }

        [ForeignKey("Villa")] public int VillaId { get; set; }

        //[ValidateNever]
        public Villa Villa { get; set; }
        public string? SpecialDetails { get; set; }
    }
}