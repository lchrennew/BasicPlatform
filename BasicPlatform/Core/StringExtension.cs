using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicPlatform.Core
{
    public static class StringExtension
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string num = "0123456789";
        const string ch = "`-=~!@#$%^&*()_+[]\\{}|;':\",./<>?";

        public static int GetPasswordStrength(this string password)
        {
            int s = 0;
            if (string.IsNullOrEmpty(password)) return 0;
            if (password.Length < 5) s += 5;
            else if (password.Length > 4 && password.Length < 8) s += 10;
            else if (password.Length > 7) s += 25;

            int upperHit = password.Hit(upper), lowerHit = password.Hit(lower), alphaHit = upperHit + lowerHit;
            if (upperHit == 0 && lowerHit != 0) s += 10;
            else if (upperHit != 0 && lowerHit != 0) s += 20;


            var numHit = password.Hit(num);
            if (numHit == 1) s += 10;
            if (numHit >= 3) s += 20;

            var chHit = password.Hit(ch);
            if (chHit == 1) s += 10;
            if (chHit > 1) s += 25;

            if (numHit != 0 && alphaHit != 0) s += 2;
            if (numHit != 0 && alphaHit != 0 && chHit != 0) s += 3;
            if (numHit != 0 && upperHit != 0 && lowerHit != 0 && chHit != 0) s += 5;
            return s;
        }

        static int Hit(this string password, string charset)
        {
            if (!string.IsNullOrEmpty(charset))
            {
                var count = 0;
                for (var i = 0; i < password.Length; i++)
                    if (charset.IndexOf(password[i]) > -1) count++;
                return count;
            }
            return 0;
        }
    }
}