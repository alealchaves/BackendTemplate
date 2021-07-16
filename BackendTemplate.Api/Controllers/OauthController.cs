using BackendTemplate.Api.Core.Controller;
using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.DTO.PerfilDTOs;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Interfaces.PerfilInterfaces;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendTemplate.Api.Controllers
{
    /// <summary>
    /// Controller de Usuario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OauthController : CustomController
    {
        private readonly ILogger<OauthController> _logger;
        public IConfiguration _Configuration { get; }

        /// <summary>
        /// Construtor do UsuarioController
        /// </summary>
        /// <param name="logger"></param>
        public OauthController(ILogger<OauthController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._Configuration = configuration;
        }

        [HttpPost("oauth")]
        [HttpOptions("oauth")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Oauth(
            [FromServices] IUsuarioSelectFacade usuarioSelectFacade,
            [FromServices] IPerfilSelectFacade perfilSelectFacade,
            UsuarioLoginRequest usuarioLoginRequest)
        {
            ServiceResult<OauthResponse> result = new ServiceResult<OauthResponse>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var JWTSecret = this._Configuration["JWTSecret"].ToString().Trim();
            var key = Encoding.ASCII.GetBytes(JWTSecret);

            var usuario = await usuarioSelectFacade.Select(usuarioLoginRequest);
            
            if (usuario.Data != null)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usuario.Data.Email));
                claims.Add(new Claim(ClaimTypes.Hash, usuario.Data.Hash.ToString()));

                foreach (var perfilUsuario in usuario.Data.UsuarioPerfis)
                {
                    var perfilRequest = new PerfilRequest(perfilUsuario.PerfilId);
                    var perfil = await perfilSelectFacade.Select(perfilRequest);
                    Claim claim = new Claim(ClaimTypes.Role, perfil.Data.Nome);
                    claims.Add(claim);
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                result.Data = new OauthResponse();
                result.Data.Token = tokenHandler.WriteToken(token);
                result.Data.Usuario = usuario.Data;

                this._logger.LogTrace(string.Format("Usuário logou {0}", result.Data.Usuario));

            }
            else
            {
                result.CodeId = usuario.CodeId;
                result.Message = usuario.Message;
                result.Errors = usuario.Errors;
            }

            return Result(result);
        }
    }
}
