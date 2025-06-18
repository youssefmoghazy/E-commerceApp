namespace Domain.Exceptions;

public sealed class BasketNotFoundExeption(string key) 
    :NoFoundException($"Basket With {key} is not found");
