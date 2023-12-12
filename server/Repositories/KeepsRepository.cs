




namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (name, description, img, creatorId)
        VALUES
        (@Name, @Description, @Img, @CreatorId);

        SELECT 
        keep.*, 
        COUNT(vKeep.id) AS kept,
        acc.*
        
        FROM keeps keep
        JOIN accounts acc ON acc.id = keep.creatorId
        
        LEFT JOIN vaultKeeps vKeep 
        ON vKeep.keepId = keep.id

        WHERE keep.id = LAST_INSERT_ID()

        GROUP BY (keep.id);";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;
    }

    internal void DestroyKeep(int keepId)
    {

        string sql = @"
        DELETE FROM keeps
        WHERE id = @keepId
        ;";

        _db.Execute(sql, new { keepId });
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT 
        keep.*, 
        COUNT(vKeep.id) AS kept,
        acc.*
        
        FROM keeps keep
        JOIN accounts acc ON acc.id = keep.creatorId
        
        LEFT JOIN vaultKeeps vKeep 
        ON vKeep.keepId = keep.id

        GROUP BY (keep.id)

        LIMIT 50
        ;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT 
        keep.*, 
        COUNT(vKeep.id) AS kept,
        acc.*
        
        FROM keeps keep
        JOIN accounts acc ON acc.id = keep.creatorId
        
        LEFT JOIN vaultKeeps vKeep 
        ON vKeep.keepId = keep.id

        WHERE keep.id = @keepId

        GROUP BY (keep.id);";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal Keep UpdateKeep(Keep keepData)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views
        WHERE id = @Id;

        SELECT 
        keep.*,
        COUNT(vKeep.id) AS kept,
        acc.*
        FROM keeps keep
        JOIN accounts acc on acc.id = keep.creatorId

        LEFT JOIN vaultKeeps vKeep 
        ON vKeep.keepId = keep.id

        WHERE keep.id = @Id

        GROUP BY (keep.id)
        ;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        string sql = @"
        SELECT 
        keep.*,
        acc.*
        FROM keeps keep
        JOIN accounts acc ON acc.id = keep.creatorId
        WHERE creatorId = @profileId
        ;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { profileId }).ToList();
        return keeps;
    }

    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}