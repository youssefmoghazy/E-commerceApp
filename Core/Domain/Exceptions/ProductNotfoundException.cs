namespace Domain.Exceptions;

public sealed class ProductNotfoundException(int id)
    : NoFoundException($"Not found product with id {id}");

