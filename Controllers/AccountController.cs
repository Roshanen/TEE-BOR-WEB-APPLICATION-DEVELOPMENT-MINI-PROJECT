using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers;

public class AccountController : BaseController
{
    private new readonly MongoContext _mongoContext;
    private readonly string _jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "";
    private readonly int _jwtExpirationDays = 1;

    public AccountController(MongoContext mongoContext) : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(User user)
    {
        try
        {
            var existingUser = await _mongoContext.GetCollection<User>("users").Find(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                ViewData["error"] = "Email already exists.";
                return View();
            }

            user.PasswordHash = HashPassword(user.PasswordHash);

            await _mongoContext.GetCollection<User>("users").InsertOneAsync(user);

            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        try
        {
            var existingUser = await _mongoContext.GetCollection<User>("users").Find(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                ViewData["error"] = "Invalid email or password.";
                return View();
            }

            if (!VerifyPassword(user.PasswordHash, existingUser.PasswordHash))
            {
                ViewData["error"] = "Invalid email or password.";
                return View();
            }

            var token = GenerateJwtToken(existingUser.Id);

            HttpContext.Session.SetString("JwtToken", token);

            var userId = _SetUserDataInViewData();

            ViewData["userId"] = userId ?? "Not logged in";

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var hashedInputPassword = HashPassword(inputPassword);
        return hashedInputPassword == hashedPassword;
    }

    private string GenerateJwtToken(ObjectId userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "Chens",
            audience: "DotnetWebApps",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_jwtExpirationDays),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
