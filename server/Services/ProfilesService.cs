



namespace Keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _repo;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesService(ProfilesRepository repo, KeepsService keepsService, VaultsService vaultsService)
    {
        _repo = repo;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        GetProfileById(profileId);
        List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
        return keeps;
    }

    internal Profile GetProfileById(string profileId)
    {
        Profile profile = _repo.GetProfileById(profileId);
        if (profile == null)
        {
            throw new Exception($"Profile: {profileId} does not exist");
        }
        return profile;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, string userId)
    {
        GetProfileById(profileId);
        List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userId);
        return vaults;
    }
}