# MarketPulse

MarketPulse is a real-time stock price tracking application built using .NET 8 with GraphQL API for the backend and React with TypeScript for the frontend. The app fetches stock prices for major companies like Apple, Microsoft, Amazon, NVIDIA, and Bitcoin in real time using the Finnhub API and provides live updates using GraphQL subscriptions.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
  - [Backend Setup (MarketPulse.Server)](#backend-setup-marketpulseserver)
  - [Frontend Setup (marketpulse.client)](#frontend-setup-marketpulseclient)
- [Usage](#usage)
- [Testing Subscriptions](#testing-subscriptions)
- [License](#license)

## Project Overview

MarketPulse provides a modern, real-time interface for tracking stock prices. The backend is powered by .NET 8, which serves as a GraphQL API using Hot Chocolate. It continuously polls the Finnhub API for the latest stock prices and pushes updates to the frontend using GraphQL subscriptions. The frontend is built using React with TypeScript and Chakra UI for styling.

## Features

- Real-time stock price updates for major companies and cryptocurrencies.
- Dynamic data fetching using the Finnhub API.
- GraphQL API with subscriptions for real-time updates.
- Modern, responsive UI using React and Chakra UI.
- Easy setup and integration with Vite for fast development.

## Technologies Used

### Backend (MarketPulse.Server)

- .NET 8
- GraphQL with Hot Chocolate
- C#
- Finnhub API
- ASP.NET Core
- In-Memory Subscriptions

### Frontend (marketpulse.client)

- React with TypeScript
- Apollo Client for GraphQL
- Chakra UI for Styling
- Vite for Development Environment
- `graphql-ws` for WebSocket communication

## Setup Instructions

### Backend Setup (MarketPulse.Server)

1. **Clone the repository**:

   ```bash
   git clone https://github.com/fredsong1972/marketpulse.git
   cd marketpulse/MarketPulse.Server
   ```

2. **Set up environment variables**:

    Create a configuration file (appsettings.json) in the MarketPulse.Server project to store your Finnhub API key:
    
    ```json
    {
      "Finnhub": {
        "ApiKey": "YOUR_API_KEY"
      }
    }
    ```

3. **Restore NuGet packages and build the project**:
    
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Run the backend server**:

   ```bash
   dotnet run
   ```
   The server will be available at http://localhost:7041.

5. **Access Banana Cake Pop (GraphQL Playground)**:
   
   Open http://localhost:7041/graphql/ui to interact with the GraphQL API.

### Frontend Setup (marketpulse.client)

1. **Navigate to the `marketpulse.client` directory**:

   ```bash
   cd ../marketpulse.client
   ```

2. **Install dependencies**:
   ```bash
   npm install
   ```

3. **Run the development server**:
   ```bash
   npm run dev
   ```
   The client will be available at http://localhost:5173.

3. **Configure Apollo Client**:

   Ensure that the Apollo Client is correctly set up in src/main.tsx with the GraphQL WebSocket endpoint:

   ```typescript
   const wsLink = new GraphQLWsLink(
      createClient({
        url: 'ws://localhost:7041/graphql', // Adjust the endpoint as needed
      })
   );
   ```

## Usage

1. **Start both the backend and frontend servers**.**
2. **Open your browser** and navigate to `http://localhost:5173` to view the real-time stock prices for Apple, Microsoft, Amazon, NVIDIA, and Bitcoin.**
3. **Interact with the UI** to see real-time updates. The prices are fetched from the Finnhub API every 10 seconds, and updates are pushed to the frontend via GraphQL subscriptions.**

## Testing Subscriptions

**To test subscriptions in Banana Cake Pop**:

1. **Go to [http://localhost:7041/graphql/ui](http://localhost:7041/graphql/ui)**
2. Open the **Subscriptions** tab and run the following subscription:

   ```graphql
   subscription OnStockPriceUpdated {
     onStockPriceUpdated {
       symbol
       currentPrice
       highPrice
       lowPrice
       previousClosePrice
       openPrice
     }
   }
   ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

