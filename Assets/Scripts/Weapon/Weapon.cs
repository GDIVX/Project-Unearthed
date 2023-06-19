using Cinemachine;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected CinemachineImpulseSource _impulseSource;
    [SerializeField] protected float _impulseForce = 0.2f;
    [SerializeField] protected float _impulseAmplitude = 0.2f;
    [SerializeField] protected float _fireRate = 2f; // Fire rate in attacks per second

    protected abstract void Fire();
}
