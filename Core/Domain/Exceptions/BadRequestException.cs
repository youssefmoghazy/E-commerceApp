namespace Domain.Exceptions;

public sealed class BadRequestException(List<string> Errors)
    : Exception("Validation Failed")
{
    public readonly List<string> Errors = Errors;


}
