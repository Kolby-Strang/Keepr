namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _a0;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _accountService = accountService;
    _a0 = auth0Provider;
    _vaultsService = vaultsService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<Vault> vaults = _vaultsService.GetVaultsByProfileId(userId);
      return Ok(vaults);
    }
    catch (Exception err)
    {
      return BadRequest(err.Message);
    }
  }
}
