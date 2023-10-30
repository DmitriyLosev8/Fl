using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    private Button _button;
    
    public int Id;
   
    public static UnityAction<int> Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        Clicked?.Invoke(Id);
        Debug.Log(Id);
    }

    public void SetWhiteColor()
    {
        _button.image.color = Color.white;
    }
    
    public void SetGreyColor()
    {
        _button.image.color = Color.grey;
    }
}


