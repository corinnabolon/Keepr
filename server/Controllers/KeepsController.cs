using System.ComponentModel;

namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepsService _keepsService;

  public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
  {
    _keepsService = keepsService;
    _auth0Provider = auth0Provider;
  }


  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _keepsService.GetKeepByIdAndIncrementViews(keepId);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.CreateKeep(keepData);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.UpdateKeep(keepId, userInfo.Id, keepData);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{keepId}")]
  public async Task<ActionResult<string>> RemoveKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _keepsService.RemoveKeep(keepId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}