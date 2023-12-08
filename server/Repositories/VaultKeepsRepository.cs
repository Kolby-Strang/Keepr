


using System.Runtime.CompilerServices;

namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {

        string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@CreatorId, @VaultId, @KeepId);

        SELECT * FROM vaultKeeps
        WHERE id = LAST_INSERT_ID()
        ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal void DestroyVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE FROM vaultKeeps
        WHERE id = @vaultKeepId
        ;";

        _db.Execute(sql, new { vaultKeepId });
    }

    internal List<FlatVaultKeep> GetKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT 
        vKeep.*,
        keep.*,
        acc.*
        FROM vaultKeeps vKeep
        JOIN keeps keep ON keep.id = vKeep.keepId
        JOIN accounts acc ON acc.id = keep.creatorId
        WHERE vKeep.vaultId = @vaultId
        ;";

        List<FlatVaultKeep> vaultKeeps = _db.Query<VaultKeep, FlatVaultKeep, Profile, FlatVaultKeep>(sql, VaultKeepBuilder, new { vaultId }).ToList();
        return vaultKeeps;
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT * FROM vaultKeeps
        WHERE id = @vaultKeepId
        ;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }

    private FlatVaultKeep VaultKeepBuilder(VaultKeep vKeep, FlatVaultKeep flatVKeep, Profile profile)
    {
        flatVKeep.Creator = profile;
        flatVKeep.VaultId = vKeep.VaultId;
        flatVKeep.VaultKeepId = vKeep.Id;
        return flatVKeep;
    }
}