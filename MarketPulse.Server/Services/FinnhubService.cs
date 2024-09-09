using System.Text.Json;
using MarketPulse.Server.Models;

namespace MarketPulse.Server.Services
{
    public class FinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FinnhubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://finnhub.io/api/v1/");
            _configuration = configuration;
        }

        public async Task<StockQuote> GetStockQuoteAsync(string symbol)
        {
            var apiKey = _configuration["Finnhub:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception("Finnhub Api Key is not configured. Pleas signup Finnhub to get your own API key.");
            }
            var response = await _httpClient.GetAsync($"quote?symbol={symbol}&token={apiKey}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var stockData = JsonSerializer.Deserialize<Dictionary<string, decimal>>(jsonResponse);
            if (stockData == null) return new StockQuote();
            return new StockQuote
            {
                Symbol = symbol,
                CurrentPrice = stockData.GetValueOrDefault("c"),
                Change = stockData.GetValueOrDefault("d"),
                PercentChange = stockData.GetValueOrDefault("dp"),
                HighPrice = stockData.GetValueOrDefault("h"),
                LowPrice = stockData.GetValueOrDefault("l"),
                OpenPrice = stockData.GetValueOrDefault("o"),
                PreviousClosePrice = stockData.GetValueOrDefault("pc")
            };
        }

        public async Task<IEnumerable<StockQuote>> GetMultipleStockQuotesAsync(List<string> symbols)
        {
            var stockQuotes = new List<StockQuote>();

            foreach (var symbol in symbols)
            {
                var quote = await GetStockQuoteAsync(symbol);
                if (quote != null)
                {
                    stockQuotes.Add(quote);
                }
            }

            return stockQuotes;
        }
    }
}
