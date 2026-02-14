using WealthStack.Domain.Exceptions;

namespace WealthStack.Domain.Entities;

public class Portfolio
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    private Portfolio() {}

    public Portfolio(string name, string description)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new RequiredFieldException(nameof(name));
        if(string.IsNullOrWhiteSpace(description)) throw new RequiredFieldException(nameof(description));

        Name = name;
        Description = description;
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }
}