using System.ComponentModel.DataAnnotations;

namespace SuperCarSpot.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a Department Name"), MaxLength(100)]
        [Display(Name ="Brand Name")]
        public string Name { get; set; }

        public List<Car>? Cars { get; set; }
    }
}
