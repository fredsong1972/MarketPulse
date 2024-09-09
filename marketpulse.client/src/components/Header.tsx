import React from 'react';
import { Flex, Heading, IconButton } from '@chakra-ui/react';
import { FaGlobeAmericas } from 'react-icons/fa';

const Header: React.FC = () => {
  return (
    <Flex align="center" justify="space-between" p={4} bg="white" shadow="md">
      <Flex align="center">
        <IconButton
          aria-label="Market Pulse Icon"
          icon={<FaGlobeAmericas />}
          isRound
          size="lg"
          colorScheme="blue"
          mr={4}
        />
        <Heading size="lg">Market Pulse</Heading>
      </Flex>
    </Flex>
  );
};

export default Header;
