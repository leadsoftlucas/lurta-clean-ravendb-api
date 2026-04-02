using LeadSoft.Common.GlobalDomain.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;

namespace Lurta.Clean.RestApi.Controllers
{
    /// <summary>
    /// 🛠️ **Controlador Raiz** - Fornece métodos utilitários, validações e funcionalidades auxiliares.
    /// </summary>
    /// <remarks>
    /// Este controlador herda de `LeadSoftRootController` e centraliza recursos auxiliares para evitar retrabalho.
    /// </remarks>
    [SwaggerTag("🛠️ **Controlador Raiz** - Fornece métodos utilitários, validações e funcionalidades auxiliares.")]
    [Route("api/")]
    [Authorize]
    public class RootController() : LeadSoftRootController(Assembly.GetExecutingAssembly())
    {
    }
}
