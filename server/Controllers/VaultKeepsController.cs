using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  private readonly VaultKeepsService _vaultKeepsService;

  public VaultKeepsController(Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
  {
    _auth0Provider = auth0Provider;
    _vaultKeepsService = vaultKeepsService;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo.Id);
      return Ok(vaultKeep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{vaultKeepId}")]

  public async Task<ActionResult<string>> RemoveVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string message = _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userId);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


}