using UnityEngine;

public abstract class InstantEffectPickup : MonoBehaviour
{
    [SerializeField] string playerTag;

    private void OnTriggerEnter(Collider playerCollision)
    {
        if (!playerCollision.CompareTag(playerTag.ToString()))
            return;
        CauseEffect(playerCollision);
    }
    protected abstract void CauseEffect(Collider playerCollision);
}
