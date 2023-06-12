using System.ComponentModel.DataAnnotations;

namespace SuperCarSpot.Models
{
    public class Company
    {
        [Range(1, 9999999, ErrorMessage = "The ID must be between 1 and 9999999. Obviously.")]
        [Display(Name ="Comapny ID")]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage =" You must provide a Company name")]
        [Display(Name ="Company Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}