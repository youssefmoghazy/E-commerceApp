namespace Domain.Models.Identity;

public class Address
{
    public int Id { get; set; }  // PK
    public string City { get; set; } = default!;
    public string street { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
    public string UserId { get; set; } // FK => UQ Index
}