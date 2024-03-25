namespace WebStore.Domain.Entities.Authentication;

public sealed class LoginModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}