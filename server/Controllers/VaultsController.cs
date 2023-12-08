using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _a0;

    public VaultsController(VaultsService vaultsService, Auth0Provider a0, VaultKeepsService vaultKeepsService)
    {
        _vaultsService = vaultsService;
        _a0 = a0;
        _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo?.Id;
            Vault vault = _vaultsService.GetVaultById(vaultId, userId);
            return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            Vault vault = _vaultsService.UpdateVault(vaultData, vaultId, userId);
            return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> DestroyVault(int vaultId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _vaultsService.DestroyVault(vaultId, userId);
            return Ok(message);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int vaultId)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo?.Id;
            List<FlatVaultKeep> vaultKeeps = _vaultKeepsService.GetKeepsByVaultId(vaultId, userId);
            return Ok(vaultKeeps);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
}