using FluentValidation;
using KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;
using KeycloakAuthIntegration.Application.CQRS.Users.UpdateUser;
using KeycloakAuthIntegration.WebApi.Common;
using MediatR;
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
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResult>), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserRequest,
        CancellationToken cancellationToken = default)
        => Created(string.Empty,
            new ApiResponseWithData<CreateUserResult>(await mediator.Send(createUserRequest, cancellationToken), true,
                "Usuário cadastrado com sucesso."));

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="id">ID of the user to update</param>
    /// <param name="updateUserRequest">Updated user data</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>The result of the update operation.</returns>
    /// <exception cref="ValidationException">Thrown when any of the validation criteria is not attended.</exception>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommand updateUserRequest,
        CancellationToken cancellationToken = default)
    {
        if (id != updateUserRequest.Id)
            throw new ValidationException("O id informado na rota é diferente do id que está sendo alterado.");
        
        return Ok(new ApiResponse((await mediator.Send(updateUserRequest, cancellationToken)).Success, "Usuário alterado com sucesso."));
    }
}