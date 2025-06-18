using System.ComponentModel.DataAnnotations;

namespace Shared.SharedTransferObjects.Authentication;

public record LoginRequest([EmailAddress] string Email, string password);
