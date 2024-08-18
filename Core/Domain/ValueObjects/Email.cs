using System.Text.RegularExpressions;

namespace AikoLearning.Core.Domain.ValuesObjects;

public class Email 
{
    #region Properties
    public string Address { get; private set; }
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );
    #endregion

    #region Constructor
    public Email(string address)
    {
        Validate(address);
        Address = address;        
    }
    #endregion

    #region Methods

    private static bool IsValid(string address)
    {
        return EmailRegex.IsMatch(address);
    }

    private void Validate(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new ArgumentException("O endereço de e-mail não pode ser nulo ou vazio");

        if (!IsValid(address))
            throw new ArgumentException($"Endereço de email '{address}' inválido");
    }

    public override bool Equals(object obj)
    {
        if (obj is Email other)
            return Address.Equals(other.Address, StringComparison.OrdinalIgnoreCase);
        

        return false;
    }

    public override int GetHashCode()
    {
        return Address.ToLowerInvariant().GetHashCode();
    }

    public override string ToString()
    {
        return Address;
    }

    public static bool operator ==(Email left, Email right)
    {
        if (ReferenceEquals(left, right))
            return true;
        
        if (left is null || right is null)
            return false;
        
        return left.Equals(right);
    }

    public static bool operator !=(Email left, Email right)
    {
        return !(left == right);
    }
    #endregion
}