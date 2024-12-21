namespace AutoParts_Backend.Models;

public class jwtOptions
{
    public record class JwtOptions(
        string Issuer,
        string Audience,
        string SigningKey,
        int ExpirationSeconds
    );
}