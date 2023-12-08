namespace Keepr.Repositories;

public class VaultKeepsRepository
{

  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }


  internal FlattenedVaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
            INSERT INTO vaultkeeps
            (vaultKeepId, vaultId)
            VALUES
            (@VaultKeepId, @VaultId);

            SELECT
            *
            FROM vaultkeeps
            WHERE vaultkeeps.id = LAST_INSERT_ID()
            ;";

    FlattenedVaultKeep flattenedVaultKeep = _db.Query<VaultKeep, FlattenedVaultKeep, FlattenedVaultKeep>(sql, (vaultKeep, flattenedVaultKeep) =>
    {
      flattenedVaultKeep.VaultKeepId = vaultKeep.Id;
      flattenedVaultKeep.VaultId = vaultKeep.VaultId;
      return flattenedVaultKeep;
    }, vaultKeepData).FirstOrDefault();

    return flattenedVaultKeep;
  }
}