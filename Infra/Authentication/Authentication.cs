using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace Infra.Authentication
{
    public class Authentication : IToken
    {

		public string TokenGenerateClient(Client client)
		{
			var claimName = client.Cpf;
			var role = client.UserRole.ToString();
			return TokenGenerate(claimName, role );
		}

		public string TokenGenerateOperator(Operator op)
		{
			var claimName = op.Registration;
			var role = "Operator";
			return TokenGenerate(claimName, role );
		}
        private string TokenGenerate(string claimName, string claimRole )
        {
                var tokenHandler = new JwtSecurityTokenHandler();
				JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"appsettings.json")));
				var key = Encoding.ASCII.GetBytes(jAppSettings["JwtToken"].ToString());
				var expirationTime = Convert.ToInt32(jAppSettings["ExpirationTime"]);                
				var tokenDescriptor = new SecurityTokenDescriptor()
				{
					Subject = new ClaimsIdentity(new Claim[]{
						new Claim(ClaimTypes.Name, claimName),
						new Claim(ClaimTypes.Role, claimRole),
					}),
					Expires = DateTime.UtcNow.AddHours(expirationTime),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return tokenHandler.WriteToken(token);
        }
    }
}