using SimpleInventory.Data;
using System.Security.Cryptography;

namespace SimpleInventory.Services
{
    public static class IdGenerator
    {
        public static string GenerateUniqueRandomId(int length)
        {
            string id;
            do
            {
                id = GenerateRandomId(length);
            } while (new ItemRepository().GetItemByIdOrName(id) != null);

            return id;
        }

        private static string GenerateRandomId(int length)
        {
            const string chars = "0123456789";
            char[] id = new char[length];
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);

            for (int i = 0; i < id.Length; i++)
            {
                id[i] = chars[randomBytes[i] % chars.Length];
            }

            return new string(id);
        }
    }
}
