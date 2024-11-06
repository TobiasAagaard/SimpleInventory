using SimpleInventory.Data;
using SimpleInventory.Models;

namespace SimpleInventory.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string? Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && _userRepository.VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                return user.Role; // Returner brugerens rolle, hvis login er succesfuldt
            }
            return null; // Returner null, hvis login fejler
        }

        public void Signup(string username, string password)
        {
            _userRepository.AddUser(username, password);
        }
    }
}
