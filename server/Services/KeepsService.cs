namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repository;

  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.CreateKeep(keepData);
    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAllKeeps();
    return keeps;
  }


  private Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetKeepById(keepId);

    if (keep == null)
    {
      throw new Exception($"Invalid Keep ID: {keepId}");
    }
    return keep;
  }

  internal Keep GetKeepByIdAndIncrementViews(int keepId)
  {
    Keep keep = GetKeepById(keepId);
    keep.Views++;
    _repository.UpdateKeep(keep);
    return keep;
  }

  internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
  {
    Keep keepToUpdate = GetKeepById(keepId);
    if (keepToUpdate.CreatorId != userId)
    {
      throw new Exception("Not your Keep to edit!");
    }

    keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
    keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
    keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;

    Keep keep = _repository.UpdateKeep(keepToUpdate);
    return keep;
  }

  internal string RemoveKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("Not your Keep to delete!");
    }
    _repository.RemoveKeep(keepId);

    return $"{keep.Name} has been deleted!";
  }
}