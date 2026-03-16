using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LearningPlatformAPI.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return (false, "Email уже используется");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = ComputeHash(dto.Password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return (true, "Пользователь зарегистрирован успешно");
    }

    public async Task<(bool IsSuccess, string Message)> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || user.PasswordHash != ComputeHash(dto.Password))
            return (false, "Неверные данные для входа");

        string token = "PLACEHOLDER_JWT";

        return (true, token);
    }

    private string ComputeHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}