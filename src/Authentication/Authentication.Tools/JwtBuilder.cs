using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Tools;

public class JwtBuilder(string issuer, string audience, string secretKey)
{
    private readonly byte[] _secretKey = Encoding.UTF8.GetBytes(secretKey);

    public string BuildJwtToken(string username, int expireMinutes = 60)
    {
        var key = new SymmetricSecurityKey(_secretKey);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string BuildJwtToken(Guid id, string role, int expireMinutes = 60)
    {
        var key = new SymmetricSecurityKey(_secretKey);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string BuildRefreshToken(Guid id)
    {
        var key = new SymmetricSecurityKey(_secretKey);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string RefreshToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
        var claims = decodedToken?.Claims;
        if (claims is null) throw new UnauthorizedAccessException("Invalid token");

        var claimsArray = claims as Claim[] ?? claims.ToArray();
        var username = claimsArray.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username)) throw new UnauthorizedAccessException("Invalid token");

        var expiration = claimsArray.FirstOrDefault(x => x.Type == ClaimTypes.Expiration)?.Value;
        var tic = long.Parse(expiration ??
                             throw new InvalidOperationException("Invalid token"));
        var expire = DateTimeOffset.FromUnixTimeSeconds(tic).UtcDateTime;
        if (expire.AddDays(1) < DateTime.UtcNow) throw new UnauthorizedAccessException("Token expired");

        return BuildJwtToken(username);
    }

    public string ControlToken(string token)
    {
        var claims = GetClaims(token).ToArray();
        if (claims is null) throw new UnauthorizedAccessException("Invalid token");

        var username = claims.FirstOrDefault(x => x.Type == "name")?.Value;
        if (string.IsNullOrEmpty(username)) throw new UnauthorizedAccessException("Invalid token");

        // control token signature
        var key = new SymmetricSecurityKey(_secretKey);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = credentials.Key,
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        var handler = new JwtSecurityTokenHandler();
        var principal = handler.ValidateToken(token, validationParameters, out _);
        if (principal is null) throw new UnauthorizedAccessException("Invalid token");

        return username;
    }

    private static IEnumerable<Claim> GetClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
        var claims = decodedToken?.Claims;
        if (claims is null) throw new UnauthorizedAccessException("Invalid token");

        return claims;
    }
}