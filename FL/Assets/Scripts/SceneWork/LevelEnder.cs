using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEnder : MonoBehaviour
{
    public static event UnityAction LevelEnded;

    public void EndLevel()
    {
        LevelEnded?.Invoke();
    }
}
