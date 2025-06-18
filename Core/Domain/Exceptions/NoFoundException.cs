namespace Domain.Exceptions;

public abstract class NoFoundException(string Message)
    : Exception(Message);

