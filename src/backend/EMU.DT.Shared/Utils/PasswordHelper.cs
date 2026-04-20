
using System.Security.Cryptography;
using System.Text;

namespace EMU.DT.Shared.Utils;

/// &lt;summary&gt;
/// 密码工具类
/// &lt;/summary&gt;
public static class PasswordHelper
{
    /// &lt;summary&gt;
    /// 哈希密码
    /// &lt;/summary&gt;
    public static string HashPassword(string password)
    {
        var salt = GenerateSalt();
        var saltedPassword = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
        var hash = SHA256.HashData(saltedPassword);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }
    
    /// &lt;summary&gt;
    /// 验证密码
    /// &lt;/summary&gt;
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split(':');
        if (parts.Length != 2)
            return false;
        
        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);
        
        var saltedPassword = Encoding.UTF8.GetBytes(password + parts[0]);
        var computedHash = SHA256.HashData(saltedPassword);
        
        return computedHash.SequenceEqual(storedHash);
    }
    
    private static byte[] GenerateSalt()
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }
}
