namespace Outthink_VendingMachine_API.DTOs
{
    public class ProductPurchaseResult
    {
        public List<CoinDTO> Change { get; set; }
        public string InfoMsg { get; set; }
    }
}
