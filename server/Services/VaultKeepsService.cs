
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

  internal FlattenedVaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    FlattenedVaultKeep flattenedVaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    Keep keep = _keepsService.GetKeepById(flattenedVaultKeep.Id);
    keep.Kept++;
    return flattenedVaultKeep;
  }
}