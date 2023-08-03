using Assets.Scripts.CharacterAbilities;
using CharacterAbilities.Assets.Scripts.CharacterAbilities;
using UnityEngine;
using Zenject;

namespace CharacterAbilities
{
    public class CharacterInstaller : MonoInstaller<CharacterInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IAimPointInput>().FromComponentSibling();

            Container.Bind<IMovementInput>().FromComponentSibling();

            Container.Bind<IProjectileWeaponInput>().FromComponentSibling();
        }
    }
}
