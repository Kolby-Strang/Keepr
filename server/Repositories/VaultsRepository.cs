



namespace Keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (creatorId, name, description, img, isPrivate)
        VALUES
        (@CreatorId, @Name, @Description, @Img, @IsPrivate);

        SELECT vault.*, 
        acc.* 
        FROM vaults vault
        JOIN accounts acc ON acc.id = vault.creatorId
        WHERE vault.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
        return vault;
    }

    internal void DestroyVault(int vaultId)
    {

        string sql = @"
        DELETE FROM vaults
        WHERE id = @vaultId
        ;";

        _db.Execute(sql, new { vaultId });
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vault.*,
        acc.* 
        FROM vaults vault
        JOIN accounts acc ON acc.id = vault.creatorId
        WHERE vault.id = @vaultId
        ;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal Vault UpdateVault(Vault vaultToUpdate)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        img = @Img,
        description = @Description,
        isPrivate = @IsPrivate
        WHERE id = @Id;

        SELECT 
        vault.*, 
        acc.*
        FROM vaults vault
        JOIN accounts acc ON acc.id = vault.creatorId
        WHERE vault.id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultToUpdate).FirstOrDefault();
        return vault;
    }

    private Vault VaultBuilder(Vault vault, Profile profile)
    {
        vault.Creator = profile;
        return vault;
    }
}