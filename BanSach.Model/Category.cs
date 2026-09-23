using System.ComponentModel.DataAnnotations;

namespace BanSach.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Display Order")]
        [Range(1, 100,ErrorMessage = "{0} chỉ có giá trị từ 1 đến 100.")]
        public int DisPlayOrder { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
