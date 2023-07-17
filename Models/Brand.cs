using System.ComponentModel.DataAnnotations;

namespace SuperCarSpot.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required(), MaxLength(250)]
        [Display(Name ="Brand Name")]
        public string Name { get; set; }

        public List<Car>? Cars { get; set; }
    }
}
