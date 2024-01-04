using LLS.Identity.Domain.Dtos;

namespace LLS.Identity.Domain.Interfaces;

public interface IJwtTokenProvider
{
    string GenerateToken(UserData userData);
}