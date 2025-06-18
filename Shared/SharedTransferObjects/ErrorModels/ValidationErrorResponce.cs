using System.Net;

namespace Shared.SharedTransferObjects.ErrorModels;

public class ValidationErrorResponce
{
    public int StatusCode {  get; set; } = (int) HttpStatusCode.BadRequest;
    public string ErrorMessage { get; set; } = "Validation Failed";
    public IEnumerable<ValidationErorrs> Erorrs { get; set; } = [];
}
