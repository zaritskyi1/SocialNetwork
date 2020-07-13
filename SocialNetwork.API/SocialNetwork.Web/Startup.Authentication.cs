using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.BLL.Security;

namespace SocialNetwork.Web
{
    public partial class Startup
    {
        public void ConfigureAuthentication(IServiceCollection services)
        {
            services.Configure<TokenSettings>(Configuration.GetSection("TokenOptions"));
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenSettings>();

            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", options =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(tokenOptions.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = key,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
