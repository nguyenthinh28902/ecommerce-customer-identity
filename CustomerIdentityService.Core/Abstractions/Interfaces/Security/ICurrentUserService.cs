namespace CustomerIdentityService.Core.Abstractions.Interfaces.Security
{
    public interface ICurrentUserService
    {
        int CustomerId { get; }
        string? Email { get; }
        string? PhoneNumber { get; }
    }
}
