namespace SuperCarSpot.Models
{
    public enum PaymentMethods
    {
        VISA,
        Mastercard,
        InteracDebit,
        Paypal,
        Stripe
    }
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FavouriteId { get; set; }
        public double Total { get; set; }
        public string ShippingAddress { get; set; }
        public bool PaymentReceived { get; set; }
        public PaymentMethods PaymentMethod { get; set;}
        public User? User { get; set; }
        public Favourite? Favourite { get; set; }
    }
}
