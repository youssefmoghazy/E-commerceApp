namespace Domain.Exceptions;

public class UserNotFoundExeption(string email)
    : NoFoundException($"No user with Email {email} was found");
