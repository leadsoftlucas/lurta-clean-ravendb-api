using LeadSoft.Common.Library.Constants;
using Lurta.Clean.Application.Contracts.Usuarios;
using Lurta.Clean.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lurta.Clean.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("👤 **Controlador de Usuários** - Gerencia o cadastro de usuários do sistema.")]
    [Authorize]
    public class UsuariosController(IUsuariosService usuarios) : ControllerBase
    {
        [HttpHead]
        [HttpGet("{id}", Name = nameof(GetUsuarioAsync))]
        [ProducesResponseType(typeof(DTOUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(Constant.ApplicationProblemJson)]
        [Consumes(Constant.ApplicationJson)]
        public async Task<ActionResult<DTOUserResponse>> GetUsuarioAsync([FromRoute] string id)
        {
            DTOUserResponse dtoResponse = await usuarios.LoadAsync(id);

            if (dtoResponse is null)
                return NotFound();

            return Ok(dtoResponse);
        }

        [HttpPost("", Name = nameof(PostUsuarioAsync))]
        [ProducesResponseType(typeof(DTOUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(Constant.ApplicationProblemJson)]
        [Consumes(Constant.ApplicationJson)]
        [AllowAnonymous]
        public async Task<ActionResult<DTOUserResponse>> PostUsuarioAsync([FromBody] DTOUserCreate dto)
        {
            DTOUserResponse dtoResponse = await usuarios.CreateAsync(dto);

            return CreatedAtAction(nameof(GetUsuarioAsync), new { id = dtoResponse.Id }, dtoResponse);
        }
    }
}
