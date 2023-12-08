


namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repo.CreateVault(vaultData);
        return vault;
    }

    internal string DestroyVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception($"Vault: {vaultId} does not belong to User: {userId}");
        }

        _repo.DestroyVault(vaultId);
        return $"Vault: {vaultId} was deleted";
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if ((vault.IsPrivate == true && vault.CreatorId != userId) || vault == null)
        {
            throw new Exception($"Vault: {vaultId} does not exist");
        }
        return vault;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        List<Vault> vaults = _repo.GetVaultsByProfileId(profileId);
        return vaults;
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId, string userId)
    {
        Vault vaultToUpdate = GetVaultById(vaultId, userId);
        if (vaultToUpdate.CreatorId != userId)
        {
            throw new Exception($"Vault: {vaultId} does not belong to User: {userId}");
        }

        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
        vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
        vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;
        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;

        Vault vault = _repo.UpdateVault(vaultToUpdate);
        return vault;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, string userId)
    {
        List<Vault> vaults = _repo.GetVaultsByProfileId(profileId);
        if (profileId == userId)
        {
            return vaults;
        }
        return vaults.FindAll(vault => vault.IsPrivate == false);
    }
}