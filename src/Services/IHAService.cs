public interface IHAService
{
    public Task PostToHA(List<Entity> entities);
}