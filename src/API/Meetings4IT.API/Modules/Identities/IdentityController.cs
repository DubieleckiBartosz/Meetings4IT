using Identities.Core.Interfaces.Services;
using Identities.Core.Models.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Meetings4IT.API.Modules.Identities;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class IdentityController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;

    public IdentityController(IUserCommandService userCommandService)
    {
        _userCommandService = userCommandService ?? throw new ArgumentNullException(nameof(userCommandService));
    }

    [HttpGet("Test")]
    public async Task<IActionResult> Test()
    {
        return Ok(new { message = "OK" });
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterUserParameters parameters)
    {
        var result = await _userCommandService.RegisterUserAsync(parameters);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserParameters parameters)
    {
        var result = await _userCommandService.UpdateDataUserAsync(parameters);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordParameters parameters)
    {
        var result = await _userCommandService.ForgotPasswordAsync(parameters);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordParameters parameters)
    {
        var result = await _userCommandService.ResetPasswordUserAsync(parameters);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("confirm-user")]
    public async Task<IActionResult> ConfirmUser(string code, string email)
    {
        var result = await _userCommandService.ConfirmUserAsync(code, email);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}