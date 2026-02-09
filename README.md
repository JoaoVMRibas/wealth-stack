# WealthStack

WealthStack is a personal investment management application designed to help track, analyze, and visualize investment portfolios in a clear and structured way.

---

## Domain

The **Domain layer** is the core of the WealthStack application.  
It represents the **business rules, concepts, and invariants** related to investment management, independent of any framework, database, or user interface.

### Core Concepts

#### Portfolio
Represents an investment wallet or grouping of assets.

- Used to organize investments into logical groups
- Examples: long-term portfolio, trading portfolio, crypto portfolio

#### Asset
Represents a financial asset that exists in the market, such as stocks, ETFs, crypto, real estate funds, or fixed income instruments.

- An `Asset` is global and does **not** belong to a portfolio
- Contains static information (name, ticker, type)
- Market price variations are stored separately as historical data

#### Transaction
Represents a financial event inside a portfolio.

- Buy or sell operations
- Always associated with:
  - One `Asset`
  - One `Portfolio`
- Transactions are immutable once created
- Financial position and balances are always derived from transactions

#### AssetHistory
Represents historical market price data for an asset.

- Stores asset price at a given reference date
- Independent from portfolios
- Used for performance analysis, charts, and metrics

### Domain Rules (Invariants)

The Domain enforces the following rules:

- Assets must have a valid name and ticker
- Portfolios must have a valid name
- Transactions must reference a valid asset and portfolio
- Quantities and prices must be greater than zero
- Fees cannot be negative
- Market prices must be greater than zero
- No financial position, balance, or profit is stored — all are calculated

Invalid states are prevented at construction time through domain validations and custom domain exceptions.

---
