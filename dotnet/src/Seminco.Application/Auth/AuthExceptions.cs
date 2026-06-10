namespace Seminco.Application.Auth;

public sealed class DuplicateCodigoDniException(string codigoDni) : Exception
{
    public string CodigoDni { get; } = codigoDni;
}

public sealed class DuplicateEmailException(string email) : Exception
{
    public string Email { get; } = email;
}
