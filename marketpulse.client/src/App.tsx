import React from 'react';
import { ChakraProvider, Container } from '@chakra-ui/react';
import Header from './components/Header';
import StockList from './components/StockList';

const App: React.FC = () => {
  return (
    <ChakraProvider>
      <Container maxW="container.xl">
        <Header />
        <StockList />
      </Container>
    </ChakraProvider>
  );
};

export default App;
