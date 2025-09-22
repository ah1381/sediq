using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Raya.Hrm.Shared.Library.Utilities
{
    public static class Generators
    {
        public static string RandIdCreator()
        {
            // new Date().getTime().toString(36) + "." + (Math.round(Math.random() * 10000)).toString(36)
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            string timestampBase36 = Base36Encode(timestamp);

            Random random = new Random();
            int randomNumber = random.Next(0, 10001); // Generates number between 0 and 10000
            string randomBase36 = Base36Encode(randomNumber);

            return $"{timestampBase36}.{randomBase36}";
        }
        private static string Base36Encode(long value)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            if (value == 0) return "0";

            string result = "";
            while (value > 0)
            {
                result = chars[(int)(value % 36)] + result;
                value /= 36;
            }

            return result;
        }
    }
}
