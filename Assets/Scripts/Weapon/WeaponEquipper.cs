using Assets.Scripts.CharacterAbilities;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class WeaponEquipper : GameObjectMount
    {

        public override void Mount(GameObject other)
        {

            base.Mount(other);

            //If other is a weapon, set the owner 

            if (other.TryGetComponent<Weapon>(out Weapon weapon))
            {
                weapon.SetOwner(Controller);
            }

            other.SetActive(true);

        }

        [Button]
        public override GameObject Unmount()
        {
            GameObject discardedObject = base.Unmount();

            discardedObject.SetActive(false);
            return discardedObject;
        }

    }
}