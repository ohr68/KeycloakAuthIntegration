using KeycloakAuthIntegration.Application.CQRS.Auth;
using KeycloakAuthIntegration.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakAuthIntegration.WebApi.Controllers;

/// <summary>
/// Handles Auth actions
/// </summary>
/// <param name="mediator">Mediator pattern used to send commands and queries to the matching handlers</param>
[ApiController]
[Route("api/[controller]")]
public class AuthController(Mediator mediator) : BaseController
{
    /// <summary>
    /// Used for user login
    /// </summary>
    /// <param name="loginRequest">Login Data</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result of the login operation. If successful will contain the token. </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthCommand loginRequest, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(loginRequest, cancellationToken));
}