



namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repo.CreateKeep(keepData);
        return keep;
    }

    internal string DestroyKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
        if (keep.CreatorId != userId)
        {
            throw new Exception($"Keep: {keepId} does not belong to User: {userId}");
        }

        _repo.DestroyKeep(keepId);

        return $"Keep: {keepId} was Deleted";
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId, bool increment = false)
    {
        Keep keep = _repo.GetKeepById(keepId);
        if (increment)
        {
            keep.Views++;
            _repo.UpdateKeep(keep);
        }
        if (keep == null)
        {
            throw new Exception($"Keep: {keepId} does not exist");
        }
        return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        List<Keep> keeps = _repo.GetKeepsByProfileId(profileId);
        return keeps;
    }

    internal Keep UpdateKeep(int keepId, Keep keepData, string userId)
    {
        Keep keepToUpdate = GetKeepById(keepId);
        if (keepToUpdate.CreatorId != userId)
        {
            throw new Exception($"Keep: {keepId} does not belong to User: {userId}");
        }

        keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
        keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;
        keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;

        Keep keep = _repo.UpdateKeep(keepToUpdate);
        return keep;
    }
}