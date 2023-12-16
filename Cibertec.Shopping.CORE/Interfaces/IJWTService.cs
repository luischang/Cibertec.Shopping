using Cibertec.Shopping.CORE.Entities;
using Cibertec.Shopping.CORE.Settings;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(User user);
    }
}