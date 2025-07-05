using System.ComponentModel.DataAnnotations;

namespace Shared.SharedTransferObjects.Authentication;

public record RegisterRequest(string Email , string DisplayName, string Password , string? UserName = "youssef", string? PhoneNumber = "");
