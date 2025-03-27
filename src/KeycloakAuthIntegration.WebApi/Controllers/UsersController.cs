using KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;
using KeycloakAuthIntegration.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakAuthIntegration.WebApi.Controllers;

/// <summary>
/// Handles User actions (CRUD)
/// </summary>
/// <param name="mediator">Mediator pattern used to send commands and queries to the matching handlers</param>
[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="createUserRequest">User data for creation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the insert operation containing the id of the created user.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserRequest,
        CancellationToken cancellationToken = default)
        => Created(string.Empty,
            new ApiResponseWithData<CreateUserResult>(await mediator.Send(createUserRequest, cancellationToken), true,
                "Usuário cadastrado com sucesso."));
}