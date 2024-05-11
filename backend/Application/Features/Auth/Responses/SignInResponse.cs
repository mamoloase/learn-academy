namespace Application.Features.Auth.Responses;
public class SignInResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpirationAt { get; set; }
    public DateTime RefreshTokenExpirationAt { get; set; }
}
