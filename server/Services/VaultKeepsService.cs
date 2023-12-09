
namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;

  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository repository, KeepsService keepsService)
  {
    _repository = repository;
    _keepsService = keepsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    Keep keep = _keepsService.GetKeepById(vaultKeep.KeepId);
    keep.Kept++;
    return vaultKeep;
  }
}