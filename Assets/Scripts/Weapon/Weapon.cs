using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField, BoxGroup("Camera Shake")] protected AnimationCurve _shakeCurve;
    [SerializeField, BoxGroup("Camera Shake")] protected float _shakeIntensity = 0.2f;
    [SerializeField, BoxGroup("Camera Shake")] protected float _shakeTime = 0.2f;
    [SerializeField, BoxGroup("Fire Rate")] protected float _fireRate = 2f; // Fire rate in attacks per second

    protected WeaponMount _mount;

    protected WeaponMount Mount { get => _mount; }

    protected abstract void Fire();

    public abstract void SetMount(WeaponMount mount);
}
