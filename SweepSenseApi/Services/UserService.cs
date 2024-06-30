using SweepSenseApi.Data;
using SweepSenseApi.Models;

namespace SweepSenseApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (user.CleaningTasks == null)
            {
                user.CleaningTasks = new List<CleaningTask>();
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            Console.WriteLine("Gebruiker verifiëren: " + username);

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("Gebruiker niet gevonden");
                return null;
            }

            Console.WriteLine("Gebruiker gevonden, wachtwoord controleren");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                Console.WriteLine("Wachtwoord komt niet overeen");
                return null;
            }

            Console.WriteLine("Wachtwoord klopt");
            return user;
        }
    }
}