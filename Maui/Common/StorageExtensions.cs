using Maui.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Common
{
    public static class StorageExtensions
    {
        public static UserDto GetUser(this ISecureStorage storage)
        {
            var tokenResponse = Task.Run(async () => await storage.GetAsync("token")).Result;

            if(tokenResponse == null)
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenResponse);
            var token = jsonToken as JwtSecurityToken;

            var id = token.Claims.First(claim => claim.Type == "id").Value;
            var email = token.Claims.First(claim => claim.Type == "email").Value;
            var phone = token.Claims.First(claim => claim.Type == "phone").Value;
            var username = token.Claims.First(claim => claim.Type == "username").Value;
            var isAdmin = token.Claims.First(claim => claim.Type == "isAdmin").Value;
            var exp = token.Claims.First(claim => claim.Type == "exp").Value;

            var expTimestamp = long.Parse(exp);

            DateTime expDate = UnixTimeStampToDateTime(expTimestamp);

            if(expDate < DateTime.Now)
            {
                SecureStorage.Default.Remove("token");
                return null;
            }

            return new UserDto
            {
                Id = int.Parse(id),
                Token = tokenResponse,
                Email = email,
                Username = username,
                Phone = phone,
                IsAdmin = bool.Parse(isAdmin)
            };

        }


        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
