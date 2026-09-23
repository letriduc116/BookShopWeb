using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ISBN { get; set; } //Gia niem yet
        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 100000000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 100000000)]
        public double Price100 { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        [ForeignKey("CoverTypeId")]
        
        public CoverType coverType { get; set; }

    }
}
