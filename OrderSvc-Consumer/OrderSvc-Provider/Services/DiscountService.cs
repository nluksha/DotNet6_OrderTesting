namespace OrderSvc_Provider.Services
{
    public class DiscountService
    {
        public double GetDiscountAmount(double customerRaing)
        {
            return customerRaing / 10;
        }
    }
}
