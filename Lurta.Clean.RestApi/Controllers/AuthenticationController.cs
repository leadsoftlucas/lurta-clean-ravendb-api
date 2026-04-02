using LeadSoft.Common.Library.Constants;
using Lurta.Clean.Application.Contracts.Authentications;
using Lurta.Clean.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lurta.Clean.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("🔐 **Controlador de Autenticação** - Gerencia login, tokens JWT e operações de segurança de contas.")]
    [Authorize]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        /// <summary>
        /// 🔑 **Login de Usuário** - Autentica o usuário e retorna um token JWT para acesso à API.
        /// </summary>
        [HttpHead]
        [HttpPost("Login", Name = nameof(PostLoginAsync))]
        [ProducesResponseType(typeof(DTOLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(Constant.ApplicationProblemJson)]
        [Consumes(Constant.ApplicationJson)]
        [AllowAnonymous]
        public async Task<ActionResult<DTOLoginResponse>> PostLoginAsync([FromBody] DTOLoginRequest dtoRequest)
            => Ok(await authenticationService.LoginAsync(dtoRequest));
    }
}
