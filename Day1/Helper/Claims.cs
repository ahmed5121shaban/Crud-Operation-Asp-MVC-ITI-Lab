using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;
using System.Security.Claims;

namespace Day1.Helper
{
    public class Claims : UserClaimsPrincipalFactory<User>
    {
        public Claims(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claims = await base.GenerateClaimsAsync(user);
            if (claims != null)
            {
                claims.AddClaim(new Claim("PhoneNumber", user.PhoneNumber??"N\\A"));
            }
            return claims;
        }
    }
}
