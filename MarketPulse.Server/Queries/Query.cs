using MarketPulse.Server.Models;
using MarketPulse.Server.Services;

namespace MarketPulse.Server.Queries
{
    public class Query
    {
        private readonly FinnhubService _finnhubService;

        public Query(FinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        [GraphQLName("stockQuote")]
        public async Task<StockQuote> GetStockQuoteAsync(string symbol)
        {
            return await _finnhubService.GetStockQuoteAsync(symbol);
        }

        [GraphQLName("hotStockQuotes")]
        public async Task<IEnumerable<StockQuote>> GetMultipleStockQuotesAsync(List<string> symbols)
        {
            return await _finnhubService.GetMultipleStockQuotesAsync(symbols);
        }
    }
}
