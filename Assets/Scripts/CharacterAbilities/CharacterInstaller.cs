using Assets.Scripts.CharacterAbilities;
using UnityEngine;
using Zenject;

namespace CharacterAbilities
{
    public class CharacterInstaller : MonoInstaller<CharacterInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IController>()
                .FromComponentSibling();
        }
    }
}
