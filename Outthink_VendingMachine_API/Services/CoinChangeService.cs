using Outthink_VendingMachine_API.Database;
using Outthink_VendingMachine_API.DTOs;

namespace Outthink_VendingMachine_API.Services
{
    public class CoinChangeService : ICoinChangeService
    {
        private readonly IVendingMachineDb _db;

        public CoinChangeService(IVendingMachineDb db)
        {
            _db = db;
        }

        public List<CoinDTO> CalculateChange(double paid, double price)
        {
            var change = paid - price;
            var changeCoins = new List<CoinDTO>();

            foreach (var coin in _db.Coins.OrderByDescending(c => c.Value))
            {
                while (coin.Quantity > 0 && change >= coin.Value)
                {
                    change = Math.Round(change - coin.Value, 2);
                    var coinInChange = changeCoins.FirstOrDefault(i => i.Value == coin.Value);
                    if (coinInChange != null)
                    {
                        coinInChange.Quantity++;
                    }
                    else
                    {
                        changeCoins.Add(new CoinDTO { Value = coin.Value, Quantity = 1 });
                    }
                }
            }

            if (change > 0)
            {
                throw new Exception("Sorry. There is not enough change available for your purchase.");
            }

            return changeCoins;
        }
    }

    public interface ICoinChangeService
    {
        public List<CoinDTO> CalculateChange(double paid, double price);
    }
}
