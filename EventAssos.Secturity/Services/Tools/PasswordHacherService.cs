using EventAssos.Core.Interfaces.Services.Tools;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EventAssos.Security.Services.Tools
{
    public class PasswordHacherService : IPasswordHacherService
    {
        private const int SaltSize = 16;      // 16 bytes pour le sel
        private const int HashSize = 32;      // 32 bytes pour le hash final
        private const int Iterations = 4;    // nombre de passages de l'algorithme
        private const int MemorySize = 65536; // mémoire utilisée (64MB) — rend le brute force coûteux
        private const int DegreeOfParallelism = 2; // nombre de threads parallèles


        public string HachPassword(string password)
        {
            //Génère un sel aléatoire cryptographiquement sûr.
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            
            //Hache le mot de passe avec ce sel
            byte[] hash = HachPasswordWithSalt(password, salt);

            //Concatène le sel + le hash dans un seul tableau (tout dans une seule string en DB.)
            byte[] combined = new byte[SaltSize + HashSize];

            Array.Copy(salt, 0, combined, 0, SaltSize);
            Array.Copy(hash, 0, combined, SaltSize, HashSize);

            return Convert.ToBase64String(combined);
        }

        private byte[] HachPasswordWithSalt(string password, byte[] salt)
        {
            //Crée un objet Argon2id 
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                //Configure l'algorithme avec les paramètres définis plus haut.
                Salt = salt,
                Iterations = Iterations,
                MemorySize = MemorySize,
                DegreeOfParallelism = DegreeOfParallelism
            };

            //Retourne le hash de 32 bytes.
            return argon2.GetBytes(HashSize);
        }

        public bool VerifyPassword(string password, string storedPassword)
        {
            //Reconvertit la string Base64 de la DB en bytes
            byte[] hashWithSalt = Convert.FromBase64String(storedPassword);

            //Extrait les 16 premiers bytes (le sel original).
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashWithSalt, 0, salt, 0, SaltSize);

            //Extrait les 32 bytes suivants (le hash original.)
            byte[] storedHash = new byte[HashSize];
            Array.Copy(hashWithSalt, SaltSize, storedHash, 0, HashSize);

            //Rehache le mot de passe entré avec le même sel
            byte[] computedHash = HachPasswordWithSalt(password, salt);

            //Compare les deux hash en temps constant
            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }
    }
}
