using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using FindWorkRazor.Data;
using FindWorkRazor.Models;

namespace FindWorkRazor.Utilites
{
    public static class Protector
    {
       // private static readonly byte[] salt = Encoding.Unicode.GetBytes("FINDWORK");

       // private static readonly int iterations = 2000;

       

        public static string[] Register(string workersalt)
        {
            // генерация соли
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
           var saltText = Convert.ToBase64String(saltBytes);
            // генерация соленого и хешированного пароля
            var sha = SHA256.Create();
            var saltedPassword = workersalt + saltText;
           
            var saltedhashedPassword = Convert.ToBase64String(
            sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            string []mas = {saltText, saltedhashedPassword};

            return mas;
        }

        public static bool CheckedPassword(Worker worker, string password)
        {
            // повторная генерация соленого и хешированного пароля
            var sha = SHA256.Create();
            var saltedPassword = password + worker.salt;
            var saltedhashedPassword = Convert.ToBase64String(
            sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
          //  return (saltedhashedPassword == worker.salt);
            return (saltedhashedPassword == worker.saltedhashedpassword);
        }

    }
}