using KeycloakAuthIntegration.Application.CQRS.Auth;
using KeycloakAuthIntegration.Application.CQRS.Auth.Login;
using KeycloakAuthIntegration.Application.CQRS.Auth.Logout;
using KeycloakAuthIntegration.Application.CQRS.Auth.RefreshToken;
using KeycloakAuthIntegration.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakAuthIntegration.WebApi.Controllers;

/// <summary>
/// Handles Auth actions
/// </summary>
/// <param name="mediator">Mediator pattern used to send commands and queries to the matching handlers</param>
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Logs the user in
    /// </summary>
    /// <param name="loginRequest">Login Data</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result of the login operation. If successful will contain the token. </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginRequest,
        CancellationToken cancellationToken = default)
        => Ok(new ApiResponseWithData<LoginResult>
        {
            Message = "Login efetuado com sucesso.",
            Data = await mediator.Send(loginRequest, cancellationToken),
            Success = true
        });

    /// <summary>
    /// Gets a new token by the Refresh token
    /// </summary>
    /// <param name="refreshTokenRequest">Refresh Token Data</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result of the refresh token operation. If successful will contain the new token. </returns>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenRequest,
        CancellationToken cancellationToken = default)
        => Ok(new ApiResponseWithData<RefreshTokenResult>
        {
            Message = "Refresh token obtido com sucesso.",
            Data = await mediator.Send(refreshTokenRequest, cancellationToken),
            Success = true
        });

    /// <summary>
    /// Logs the user out
    /// </summary>
    /// <param name="logoutRequest">Logout data containing the user id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result of the logout operation.</returns>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand logoutRequest,
        CancellationToken cancellationToken = default)
        => Ok(new ApiResponse
        {
            Message = "Logout efetuado com sucesso.",
            Success = (await mediator.Send(logoutRequest, cancellationToken)).Success
        });
}