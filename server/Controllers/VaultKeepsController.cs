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

  public async Task<ActionResult<FlattenedVaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      FlattenedVaultKeep flattenedVaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
      return Ok(flattenedVaultKeep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }

  }


}