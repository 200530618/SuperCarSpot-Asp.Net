using Microsoft.AspNetCore.Identity;

namespace SuperCarSpot.Models

{
    public class User : IdentityUser
    {
        public List<Favourite>? Favourite { get; set; }
        public List<Order>? Order { get; set; }
    }
}
