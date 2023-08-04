namespace SuperCarSpot.Models
{
    public class FavouriteItem
    {
        public int Id { get; set; }
        public int FavouriteId { get; set; }
        public int CarId { get; set; }
        public double Price { get; set; }
        public Favourite? Favourite { get; set; }
        public Car? Car { get; set; }   
    }
}