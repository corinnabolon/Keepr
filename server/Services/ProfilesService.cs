
namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repository;

  public ProfilesService(ProfilesRepository repository)
  {
    _repository = repository;
  }

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _repository.GetProfileById(profileId);
    return profile;
  }
}