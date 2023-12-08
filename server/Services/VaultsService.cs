

namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repository;

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _repository.GetVaultById(vaultId);

    if (vault == null)
    {
      throw new Exception($"Invalid vault ID: {vaultId}");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("Something went wrong...");
    }

    return vault;
  }

  internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
  {
    Vault vaultToUpdate = GetVaultById(vaultId, userId);

    if (vaultToUpdate.CreatorId != userId)
    {
      throw new Exception("Not your Vault to edit!");
    }

    vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
    vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
    vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
    vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

    Vault vault = _repository.UpdateVault(vaultToUpdate);
    return vault;
  }

  internal string RemoveVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Not your Vault to delete!");
    }

    _repository.RemoveVault(vaultId);

    return $"{vault.Name} has been deleted!";
  }



}