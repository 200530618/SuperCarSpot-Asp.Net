namespace SuperCarSpot.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; } = true;
        public User? User { get; set; }
        public List<FavouriteItem>? FavouriteItems { get; set; }
        public Order? Order { get; set; }
    }
}
