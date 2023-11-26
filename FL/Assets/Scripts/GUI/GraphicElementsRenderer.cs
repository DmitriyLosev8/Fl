using UnityEngine;
using Assets.Scripts.DoorSystem;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.GUI
{
    public class GraphicElementsRenderer : MonoBehaviour
    {
        [SerializeField] private Transform _keySpace;
        [SerializeField] private DoorPointer _pointer;

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

        private void EnablePointer()
        {
            _pointer.gameObject.SetActive(true);
        }

        private void DisablePointer()
        {
            _pointer.gameObject.SetActive(false);
        }

        private void OnKeyPickedUp(Key key)
        {
            key.transform.position = _keySpace.position;
            key.transform.rotation = _keySpace.rotation;
            key.transform.SetParent(_keySpace);
            EnablePointer();
        }

        private void OnCharacterWalkedOutADoor()
        {
            DisablePointer();
            Destroy(GetComponent<Key>());
        }
    }
}