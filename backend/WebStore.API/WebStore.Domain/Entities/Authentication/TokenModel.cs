namespace WebStore.Domain.Entities.Authentication;

public sealed class TokenModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}