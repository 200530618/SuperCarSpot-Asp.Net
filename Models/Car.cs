using System.ComponentModel.DataAnnotations;

namespace SuperCarSpot.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        [Display(Name ="Car Name or Model")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Year Of Manufacturing")]
        public int Year { get; set; }
        [Required, MaxLength(4000)]
        [Display(Name ="Car Description")]
        public string Description { get; set; }
        [Required, MaxLength(250)]
        [Display(Name ="Car Color")]
        public string Color { get; set; }
        [Required, Range(0.01,9999999999.99)]
        [Display(Name ="Expected Price Of Car In CAD")]
        public double Price { get; set; }
        [Required, MaxLength(250)]
        [Display(Name ="Name Of Car Owner")]
        public string OwnerName { get; set; }
        [Required, MaxLength(2000)]
        [Display(Name ="Address")]
        public string Address { get; set; }
        [Required, MaxLength(250)]
        public string City { get; set; }
        [Required, MaxLength(250)]
        public string PostalCode { get; set; }
        [Required, MaxLength(250)]
        [Display(Name ="Email of Owner")]
        public string Email { get; set; }
        public string? Photo { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public Brand? Brand { get; set; }
    }
}
