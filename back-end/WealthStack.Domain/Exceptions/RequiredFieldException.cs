namespace WealthStack.Domain.Exceptions;

public class RequiredFieldException : DomainException
{
    public string FieldName { get;}
    public RequiredFieldException(string fieldName) 
        : base($"The '{fieldName}' field must be filled in, it cannot be empty.")
    {
        FieldName = fieldName;
    }
}