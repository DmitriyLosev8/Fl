using UnityEngine;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.SceneWork
{
    public class FinishLevelZone : MonoBehaviour
    {
        [SerializeField] private LevelEnder _levelChanger;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                _levelChanger.EndLevel();
            }      
        }
    }
}