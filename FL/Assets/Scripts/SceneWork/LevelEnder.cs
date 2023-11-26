using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.SceneWork
{
    public class LevelEnder : MonoBehaviour
    {
        public static event UnityAction LevelEnded;

        public void EndLevel()
        {
            LevelEnded?.Invoke();
        }
    }
}