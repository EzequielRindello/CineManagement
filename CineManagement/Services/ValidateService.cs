namespace CineManagement.Services
{
    public class ValidationService
    {
        public static bool ValidateDateTime(DateTime dateTime)
        {
            return dateTime != default && dateTime > DateTime.Now;
        }

        public static bool ValidatePrice(decimal price)
        {
            return price > 0;
        }
    }
}
