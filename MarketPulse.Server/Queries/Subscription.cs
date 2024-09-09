using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using MarketPulse.Server.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MarketPulse.Server.Queries
{
    public class Subscription
    {
        [Subscribe]
        [Topic("StockPriceUpdated")]
        public StockQuote OnStockPriceUpdated([EventMessage] StockQuote stockQuote)
        {
            return stockQuote;
        }
    }
}
