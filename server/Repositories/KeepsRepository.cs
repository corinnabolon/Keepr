

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
      INSERT INTO
      keeps (name, description, img, creatorId)
      VALUES (@Name, @Description, @Img, @CreatorId);
      
      SELECT
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.id = LAST_INSERT_ID();";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, keepData).FirstOrDefault();

    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }).ToList();

    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
     SELECT
     keeps.*,
     accounts.*
     FROM keeps
     JOIN accounts ON accounts.id = keeps.creatorId
     WHERE keeps.id = @keepId;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { keepId }).FirstOrDefault();

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
      views = @Views,
      kept = @Kept
      WHERE id = @Id LIMIT 1;

      SELECT 
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.id = @Id;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, keepData).FirstOrDefault();

    return keep;
  }


  internal void RemoveKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
    _db.Execute(sql, new { keepId });
  }

  internal List<KeepInVault> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"SELECT
      vaultkeeps.*,
      keeps.*,
      accounts.*
      FROM vaultkeeps
      JOIN keeps ON keeps.id = vaultkeeps.keepId
      JOIN accounts ON accounts.id = vaultkeeps.creatorId
      WHERE vaultkeeps.vaultId = @vaultId;";


    List<KeepInVault> keepsInVault = _db.Query<VaultKeep, KeepInVault, Profile, KeepInVault>(sql, (vaultkeep, keepinvault, profile) =>
    {
      keepinvault.VaultKeepId = vaultkeep.Id;
      keepinvault.VaultId = vaultkeep.VaultId;
      keepinvault.Creator = profile;
      return keepinvault;

    }, new { vaultId }).ToList();
    return keepsInVault;
  }

  internal List<Keep> GetUserKeeps(string profileId)
  {
    string sql = @"
      SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id
    WHERE accounts.id = @profileId;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();

    return keeps;
  }
}