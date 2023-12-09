

namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;

  private readonly KeepsService _keepsService;

  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repository, KeepsService keepsService, VaultsService vaultsService)
  {
    _repository = repository;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Not your vault.");
    }
    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    Keep keep = _keepsService.GetKeepById(vaultKeep.KeepId);
    keep.Kept++;
    _keepsService.UpdateKeptCount(keep);
    return vaultKeep;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception($"Invalid ID: {vaultKeepId}");
    }
    return vaultKeep;
  }

  internal string RemoveVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("Not your vaultkeep!");
    }
    Keep keep = _keepsService.GetKeepById(vaultKeep.KeepId);
    keep.Kept--;
    _keepsService.UpdateKeptCount(keep);
    _repository.RemoveVaultKeep(vaultKeepId);
    return "VaultKeep deleted.";
  }
}