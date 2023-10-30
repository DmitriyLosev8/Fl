using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelZone : MonoBehaviour
{
    [SerializeField] private LevelChanger _levelChanger;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            _levelChanger.EndLevel();
    }
}
