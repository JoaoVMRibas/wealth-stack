using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;

namespace WealthStack.Domain.Entities;

public class Asset
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Ticker { get; private set; } = string.Empty;
    public AssetType Type { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    private readonly List<AssetHistory> _history = [];
    public IReadOnlyCollection<AssetHistory> History => _history.AsReadOnly();
    
    private Asset() {}

    public Asset(string ticker, string name, AssetType type)
    {
        Validate(ticker,name);
        
        Ticker = ticker.Trim().ToUpper();
        Name = name.Trim();
        Type = type;
        IsActive = true;
    }

    public void Update(string ticker, string name, AssetType type)
    {
        Validate(ticker,name);

        Ticker = ticker.Trim().ToUpper();
        Name = name.Trim();
        Type = type;
    }

    private static void Validate(string ticker, string name)
    {
        if(string.IsNullOrWhiteSpace(ticker)) throw new RequiredFieldException(nameof(ticker));
        if(string.IsNullOrWhiteSpace(name)) throw new RequiredFieldException(nameof(name));
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}