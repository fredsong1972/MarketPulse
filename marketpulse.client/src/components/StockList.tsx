import React, { useEffect, useState } from 'react';
import { Box, Flex, Heading, Text, VStack, HStack, Badge } from '@chakra-ui/react';
import { useQuery, useSubscription, gql } from '@apollo/client';
import { StockQuote } from '../StockTypes';

// GraphQL query to get initial stock prices
const GET_STOCK_PRICES = gql`
  query GetStockPrices {
    hotStockQuotes(symbols: ["AAPL", "MSFT", "AMZN", "NVDA", "BTC-USD"]) {
      symbol
      currentPrice
      change
      percentChange
      highPrice
      lowPrice
      previousClosePrice
      openPrice
    }
  }
`;

// GraphQL subscription for real-time updates
const STOCK_PRICE_SUBSCRIPTION = gql`
  subscription OnStockPriceUpdated {
    onStockPriceUpdated {
      symbol
      currentPrice
      change
      percentChange
      highPrice
      lowPrice
      previousClosePrice
      openPrice
    }
  }
`;

const StockList: React.FC = () => {
  const { loading, error, data } = useQuery<{ hotStockQuotes: StockQuote[] }>(GET_STOCK_PRICES);
  const { data: subscriptionData } = useSubscription<{ onStockPriceUpdated: StockQuote }>(STOCK_PRICE_SUBSCRIPTION);
  const [stocks, setStocks] = useState<StockQuote[]>([]);

  useEffect(() => {
    if (data) {
      setStocks(data.hotStockQuotes);
    }
  }, [data]);

  useEffect(() => {
    console.log('Subscription data received:', subscriptionData); 
    if (subscriptionData) {
      const updatedStock = subscriptionData.onStockPriceUpdated;
      console.log('Updated stock:', updatedStock); // Log the updated stock data
      setStocks((prevStocks) =>
        prevStocks.map((stock) =>
          stock.symbol === updatedStock.symbol ? updatedStock : stock
        )
      );
    }
  }, [subscriptionData]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading stock prices.</p>;

  return (
    <VStack spacing={6} align="start" p={4}>
      <Heading size="md" mb={4}>
        Hot Indices
      </Heading>
      <Flex wrap="wrap" justify="space-between" width="100%">
        {stocks.map((stock) => (
          <Box
            key={stock.symbol}
            p={4}
            shadow="md"
            borderWidth="1px"
            borderRadius="lg"
            width="30%"
            mb={4}
          >
            <HStack justify="space-between">
              <Text fontWeight="bold">{stock.symbol}</Text>
              <Badge colorScheme={stock.currentPrice >= stock.previousClosePrice ? 'green' : 'red'}>
                {stock.percentChange > 0 ? '+' : ''}
                {(stock.percentChange).toFixed(2)}%
              </Badge>
            </HStack>
            <Text mt={2}>Price: ${stock.currentPrice.toFixed(2)}</Text>
            <Text>High: ${stock.highPrice.toFixed(2)}</Text>
            <Text>Low: ${stock.lowPrice.toFixed(2)}</Text>
          </Box>
        ))}
      </Flex>
    </VStack>
  );
};

export default StockList;
