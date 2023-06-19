using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    float FireRate { get; } // Fire rate in attacks per second

    protected abstract void Fire();
}
