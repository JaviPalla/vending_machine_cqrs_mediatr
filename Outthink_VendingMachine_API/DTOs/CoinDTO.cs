namespace Outthink_VendingMachine_API.DTOs
{
    public class CoinDTO
    {
        public double Value { get; set; }
        public double Quantity { get; set; }

        public override string ToString()
        {
            return $"{Quantity} coin/s of {Value} EUR";
        }
    }
}
