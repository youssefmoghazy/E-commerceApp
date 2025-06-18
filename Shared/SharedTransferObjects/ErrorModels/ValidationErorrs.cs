namespace Shared.SharedTransferObjects.ErrorModels
{
    public class ValidationErorrs
    {
        public string Field { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}