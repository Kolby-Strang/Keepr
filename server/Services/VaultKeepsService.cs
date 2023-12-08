


namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception($"Vault: {vault.Id} does not belong to User: {vaultKeepData.CreatorId}");
        }
        VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal string DestroyVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception($"VaultKeep: {vaultKeepId} does not belong to User: {userId}");
        }
        _repo.DestroyVaultKeep(vaultKeepId);
        return $"VaultKeep: {vaultKeepId} was Deleted";
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"VaultKeep: {vaultKeepId} does not exist");
        }
        return vaultKeep;
    }

    internal List<FlatVaultKeep> GetKeepsByVaultId(int vaultId, string userId)
    {
        _vaultsService.GetVaultById(vaultId, userId);//Checks for privacy and Existence
        List<FlatVaultKeep> vaultKeeps = _repo.GetKeepsByVaultId(vaultId);
        return vaultKeeps;
    }
}