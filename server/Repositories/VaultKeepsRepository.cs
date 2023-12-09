
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
            INSERT INTO vaultkeeps
            (creatorId, keepId, vaultId)
            VALUES
            (@CreatorId, @KeepId, @VaultId);

            SELECT * FROM vaultkeeps
            WHERE vaultkeeps.id = LAST_INSERT_ID()
            ;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();

    return vaultKeep;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = "SELECT * FROM vaultkeeps WHERE id = @vaultKeepId;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();

    return vaultKeep;
  }

  internal void RemoveVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vaultkeeps WHERE id = @vaultKeepId LIMIT 1;";
    _db.Execute(sql, new { vaultKeepId });
  }


  // internal KeepInVault CreateVaultKeep(VaultKeep vaultKeepData)
  // {
  //   string sql = @"
  //           INSERT INTO vaultkeeps
  //           (creatorId, keepId, vaultId)
  //           VALUES
  //           (@CreatorId, @KeepId, @VaultId);

  //           SELECT
  //           vaultkeeps.*,
  //           keeps.*
  //           FROM vaultkeeps
  //           JOIN keeps ON keeps.id = vaultkeeps.keepId
  //           WHERE vaultkeeps.id = LAST_INSERT_ID()
  //           ;";

  //   KeepInVault keepInVault = _db.Query<VaultKeep, KeepInVault, KeepInVault>(sql, (vaultKeep, keepInVault) =>
  //   {
  //     keepInVault.VaultKeepId = vaultKeep.Id;
  //     keepInVault.VaultId = vaultKeep.VaultId;
  //     return keepInVault;
  //   }, vaultKeepData).FirstOrDefault();

  //   return keepInVault;
  // }
}