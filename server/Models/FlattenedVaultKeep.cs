namespace Keepr.Models;

public class FlattenedVaultKeep : Keep
{
  public int VaultKeepId { get; set; }

  public int VaultId { get; set; }
}