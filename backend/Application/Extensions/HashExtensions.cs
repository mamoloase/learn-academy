
namespace Application.Extensions;
public class HashExtensions
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
