using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProtocoloConsultasAPI;

public class JwtAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var token = filterContext.HttpContext.Request.Headers["Authorization"]?.Split(' ').Last();

        if (token == null || !ValidateToken(token))
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }

    private bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAuthentication.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
