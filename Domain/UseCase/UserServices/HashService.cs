using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Domain.UseCase.UserServices
{
    class HashPasswordService
    {
        private HashAlgorithm _algorithm;

        public HashPasswordService()
        {
            _algorithm = SHA512.Create();
        }

        public string EncryptPassword(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool CheckPassword(string passwordType, string password)
        {
            if (string.IsNullOrEmpty(passwordType))
                throw new NullReferenceException("Informe uma senha.");

            if (string.IsNullOrEmpty(password))
                throw new NullReferenceException("Cadastre uma senha.");

            var encryptedPassword = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordType));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == password;
        }
    }
}
