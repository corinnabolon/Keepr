namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly ProfilesService _profilesService;

  private readonly KeepsService _keepsService;

  private readonly VaultsService _vaultsService;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, KeepsService keepsService, VaultsService vaultsService)
  {
    _profilesService = profilesService;
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [HttpGet("{profileId}/keeps")]

  public ActionResult<List<Keep>> GetUserKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetUserKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]

  public async Task<ActionResult<List<Vault>>> GetUserVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetUserVaults(profileId, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


}