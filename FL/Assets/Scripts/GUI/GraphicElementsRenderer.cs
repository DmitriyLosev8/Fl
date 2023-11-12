using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicElementsRenderer : MonoBehaviour
{
    [SerializeField] private Transform _keySpace;
    [SerializeField] private Arrow _arrow;

    private void OnEnable()
    {
        Player.KeyPickedUp += OnKeyPickedUp;
        Player.CharacterWalkedOutADoor += OnCharacterWalkedOutADoor;
    }

    private void OnDisable()
    {
        Player.KeyPickedUp -= OnKeyPickedUp;
        Player.CharacterWalkedOutADoor -= OnCharacterWalkedOutADoor;
    }

    private void EnableArrow()
    {
        _arrow.gameObject.SetActive(true);
    }

    private void DisableArrow()
    {
        _arrow.gameObject.SetActive(false);
    }

    private void OnKeyPickedUp(Key key)
    {
        key.transform.position = _keySpace.position;
        key.transform.rotation = _keySpace.rotation;
        key.transform.SetParent(_keySpace);
        EnableArrow();
    }

    private void OnCharacterWalkedOutADoor()
    {
        DisableArrow();
        Destroy(GetComponent<Key>());
    }
}
