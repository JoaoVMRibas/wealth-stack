namespace WealthStack.Domain.Exceptions;

public class InvalidValueException : DomainException
{
    public InvalidValueException(string message) : base(message) {}

    public static InvalidValueException QuantityMustBeGreaterThanZero()
        => new("The quantity must be greater than zero.");

    public static InvalidValueException UnitPriceMustBeGreaterThanZero()
        => new("The price per unit must be greater than zero.");

    public static InvalidValueException FeesCannotBeNegative()
        => new("Fees cannot be negative.");

    public static InvalidValueException PriceMustBeGreaterThanZero()
        => new("Price must be greater than zero.");
}