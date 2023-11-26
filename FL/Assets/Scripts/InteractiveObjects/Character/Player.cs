using System;
using UnityEngine;
using Assets.Scripts.AuxiliaryComponents;
using Assets.Scripts.Containers;
using Assets.Scripts.DoorSystem;

namespace Assets.Scripts.InteractiveObjects.Character
{
    [RequireComponent(typeof(LightContainer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Oxygen _oxygen;
        [SerializeField] private ParticleSystem _effectOfDying;
        [SerializeField] private Transform _raycastStartPoint;
        [SerializeField] private GameObject _laternSpotLight;

        private int _defaultValueOfCurrentKeyId = 100;
        private float _positionYToSelfDesttoy = -30f;
        private float _oxygenLosingDamage;
        private float _healthLosingDamage;  
        private int _maxValueOfLigth = 50;
        private float _lowOxygenDamage = 0.4f;
        private float _lowHealthDamage = 0.1f;
        private float _middleOxygenDamage = 0.6f;
        private float _middleHealthDamage = 0.2f; 
        private float _hightOxygenDamage = 1f;
        private float _hightHealthDamage = 0.4f;
        private int _lowLevels = 2;
        private int _highLevels = 6;
        private float _distanceToSpotResourseOrb = 3.5f;
        private Light _spotLight;
        private LightContainer _lightContainer;

        public static Action Died;
        public static event Action<Key> KeyPickedUp;
        public static event Action CharacterWalkedOutADoor;

        public int Level { get; private set; }
        public bool IsSpotedResoursesOrb { get; private set; }
        public int CurrentKeyID { get; private set; }
        public bool IsHaveKey { get; private set; }
        public float Health => _health.Value;
        public float Oxygen => _oxygen.Value;

        private void Awake()
        {
            _lightContainer = GetComponent<LightContainer>();
            _spotLight = _laternSpotLight.GetComponent<Light>();
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(SavesTitles.Level))
            {
                Level = PlayerPrefs.GetInt(SavesTitles.Level);
            }

            SetEternalDamage();
            SetStartSpotLightAngle();
        }

        private void Update()
        {
            if (transform.position.y <= _positionYToSelfDesttoy)
            {
                Die();
            }

            SpotResourceOrb();
            TakeEternalDamagePerTime();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Key key))
            {
                PickUpKey(key);
            }    
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Door door))
            {
                if (door.Id == CurrentKeyID)
                {
                    IsHaveKey = false;
                    CharacterWalkedOutADoor?.Invoke();
                    CurrentKeyID = _defaultValueOfCurrentKeyId;
                }
            }
        }

        public void TakeHealthDamage(float healthDamage)
        {
            _health.TakeDamage(healthDamage);

            if (_health.Value <= 0)
            {
                Die();
            }      
        }

        public void TakeOxygenDamage(float oxygenDamage)
        {
            _oxygen.TakeDamage(oxygenDamage);
        }

        public void TakeLightDamage(float lightDamage)
        {
            if (_spotLight.spotAngle > 0)
            {
                _spotLight.spotAngle -= lightDamage;
            }
        }

        public void TakeOxygen(float valueOfOxygen)
        {
            _oxygen.RaiseValue(valueOfOxygen);
        }

        public void TakeLight(float valueOfLight)
        {          
            if (_spotLight.spotAngle < _maxValueOfLigth)
            {
                _spotLight.spotAngle += valueOfLight;
            }
            
            CollectLightOrb();
        }

        private void SetStartSpotLightAngle()
        {
            if (PlayerPrefs.HasKey(SavesTitles.LaternAngle))
                _spotLight.spotAngle = PlayerPrefs.GetInt(SavesTitles.LaternAngle);
        }

        private void SetEternalDamage()
        {
            if (Level <= _lowLevels)
            {
                _oxygenLosingDamage = _lowOxygenDamage;
                _healthLosingDamage = _lowHealthDamage;
            }
            else if(Level >= _highLevels)
            {
                _oxygenLosingDamage = _hightOxygenDamage;
                _healthLosingDamage = _hightHealthDamage;
            }
            else
            {
                _oxygenLosingDamage = _middleOxygenDamage;
                _healthLosingDamage = _middleHealthDamage;
            }
        }

        private void SpotResourceOrb()
        {
            Ray rayToSpot = new Ray(_raycastStartPoint.transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(rayToSpot, out hit, _distanceToSpotResourseOrb))
            {
                ResourceOrb orb = hit.collider.gameObject.GetComponent<ResourceOrb>();

                if (orb)
                {
                    IsSpotedResoursesOrb = true;
                    orb.DeterminePlayer(this);
                }
            }
        }

        private void CollectLightOrb()
        {
            int lightOrb = 1;
            _lightContainer.ApplyLights(lightOrb);
        }

        private void PickUpKey(Key key)
        {
            if (!IsHaveKey)
            {
                KeyPickedUp?.Invoke(key);
                IsHaveKey = true;
                CurrentKeyID = key.Id;
            }
        }

        private void Die()
        {     
            if (_health.Value <= 0)
            {
                Instantiate(_effectOfDying, transform.position, Quaternion.identity);
                Died?.Invoke();
                Destroy(gameObject);
            }
        }

        private void TakeEternalDamagePerTime()
        {
            if (Time.timeScale == 1)
            {
                if (_oxygen.Value <= 0)
                {
                    SlowlyDie();
                }
                else
                {
                    _oxygen.TakeEternalDamage(_oxygenLosingDamage);
                }
            }
        }

        private void SlowlyDie()
        {
            _health.TakeEternalDamage(_healthLosingDamage);
            Die();
        }
    }
}