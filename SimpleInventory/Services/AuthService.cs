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

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null)
            {
                return _userRepository.VerifyPassword(password, user.PasswordHash, user.Salt);
            }
            return false;
        }

        public void Signup(string username, string password)
        {
            _userRepository.AddUser(username, password);
        }
    }
}
