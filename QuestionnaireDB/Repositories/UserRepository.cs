using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class UserRepository
    {
        public List<User> GetAll()
        {
            using (var db = new QuestionnaireDBContext())
            {
                var query = from u in db.User
                            select u;
                return query.ToList();
            }
        }

        public User Get(string username, string passwordStr)
        {
            User user = null;
            using (var db = new QuestionnaireDBContext())
            {
                var query = from u in db.User
                            where u.Username == username
                            select u;
                user = query.SingleOrDefault();
            }
            if (user == null) return null;      // no user found

            byte[] password = GenerateSaltedHash(Encoding.ASCII.GetBytes(passwordStr), user.Salt);

            if (CompareByteArrays(user.Password,password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool Add(string username, string passwordStr, int roleId)
        {
            bool alreadyExists = false;
            using (var db = new QuestionnaireDBContext())
            {
                var query = from u in db.User
                            where u.Username == username
                            select u;
                alreadyExists = query.Any();
            }
            if (alreadyExists) return false;

            byte[] salt = GetSalt();
            byte[] password = GenerateSaltedHash(Encoding.ASCII.GetBytes(passwordStr),salt);
            try
            {
                using (var db = new QuestionnaireDBContext())
                {
                    db.User.Add(new User()
                    {
                        Username = username,
                        Password = password,
                        Salt = salt,
                        RoleId = roleId
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] GetSalt()
        {
            int maximumSaltLength = 32;
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
