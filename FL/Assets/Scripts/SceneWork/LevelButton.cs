using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private int _id;
    
    private Button _button;
    
    public static Action<int> Clicked;

    public int Id => _id;

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


