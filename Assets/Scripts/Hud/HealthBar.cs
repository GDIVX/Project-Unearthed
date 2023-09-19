using Assets.Scripts.CharacterAbilities;

namespace Unearthed
{
    public class HealthBar : HUDBars
    {
        private void OnValidate()
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            Value = playerController.GetComponent<Health>().Value;
            MaxValue = playerController.GetComponent<Health>().MaxValue;
        }
    }
}
