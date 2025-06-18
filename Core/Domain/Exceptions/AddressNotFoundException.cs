namespace Domain.Exceptions;

public sealed class AddressNotFoundException(string UserName)
    : NoFoundException($"User {UserName} has no ShipToAddress ");