using ObjectPooling;
using UnityEngine;

public abstract class InstantEffectPickup : MonoBehaviour, IPoolable
{
    [SerializeField] string _playerTag;
    protected GameObject _gameObject { get; }

    ObjectSpawner spawner;

    public ObjectSpawner Spawner
    {
        get => spawner;
        set
        {
            spawner = value;
        }
    }

    public GameObject GameObject => this.gameObject;

    private void OnTriggerEnter(Collider playerCollision)
    {
        if (!playerCollision.CompareTag(_playerTag.ToString()))
            return;
        CauseEffect(playerCollision);
    }
    protected abstract void CauseEffect(Collider playerCollision);

    public void OnGet()
    {
        
    }

    public void OnReturn()
    {
        
    }
}
