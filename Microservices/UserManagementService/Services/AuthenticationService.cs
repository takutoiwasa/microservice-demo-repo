using UserManagementService.Data;
using UserManagementService.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace UserManagementService.Services
{
    public class AuthenticationService
    {
        private readonly UserContext _context;

        public AuthenticationService(UserContext context)
        {
            _context = context;
        }

        public async Task<Session> CreateSessionAsync(int userId)
        {
            var session = new Session
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddHours(1)
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<bool> ValidateSessionAsync(string token)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Token == token && s.Expiration > DateTime.UtcNow);

            return session != null;
        }

        public async Task LogoutAsync(string token)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Token == token);

            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }
    }
}