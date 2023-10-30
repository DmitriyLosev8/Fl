using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialPanel : MonoBehaviour
{
    public static UnityAction LoadMainMenuButtonClicked;

    public void OnLoadMainMenuButtonClicked()
    {
        Time.timeScale = 1;
        LoadMainMenuButtonClicked?.Invoke();
    }
}
