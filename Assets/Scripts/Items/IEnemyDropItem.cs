using ObjectPooling;

public interface IEnemyDropItem : IPoolable
{
    public bool WillDrop();
}
