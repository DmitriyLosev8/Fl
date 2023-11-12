using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LightContainer))]
public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Oxygen _oxygen;
    [SerializeField] private ParticleSystem _effectOfDying;
    [SerializeField] private Transform _raycastStartPoint;
    [SerializeField] private GameObject _laternSpotLight;

    private int _defaultValueOfCurrentKeyId = 100;
    private Light _spotLight;
    private float _positionYToSelfDesttoy = -30f;
    private float _oxygenLosingDamage; 
    private float _healthLosingDamage;
    private int _startSpotLightAngle;
    private LightContainer _lightContainer;
    
    public static event UnityAction Died;
    public static event UnityAction<Key> KeyPickedUp;
    public static event UnityAction CharacterWalkedOutADoor;

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
        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Level))
            Level = UnityEngine.PlayerPrefs.GetInt(KeySave.Level);
           
        SetEternalDamage();
        SetStartSpotLightAngle();
    }

    private void Update()
    {
        if (transform.position.y <= _positionYToSelfDesttoy)
            Die();

        TryToCollectResourseOrb();
        TakeEternalDamagePerTime();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Key key))
            PickUpKey(key);
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
            Die();
    }

    public void TakeOxygenDamage(float oxygenDamage)
    {
       _oxygen.TakeDamage(oxygenDamage);
    }

    public void TakeLightDamage(float lightDamage)
    {
        if (_spotLight.spotAngle > 0)
            _spotLight.spotAngle -= lightDamage;
    }

    public void TakeOxygen(float valueOfOxygen)
    {
        _oxygen.RaiseValue(valueOfOxygen);
    }

    public void TakeLight(float valueOfLight)
    {
        int maxValueOfLigth = 50;

        if (_spotLight.spotAngle < maxValueOfLigth)
            _spotLight.spotAngle += valueOfLight;

        CollectLightOrb();
    }

    private void SetStartSpotLightAngle()
    {
            if (UnityEngine.PlayerPrefs.HasKey(KeySave.LaternAngle))
                _spotLight.spotAngle = UnityEngine.PlayerPrefs.GetInt(KeySave.LaternAngle);    
    }
    
    private void SetEternalDamage()
    {
        float lowOxygenDamage = 0.4f;
        float lowHealthDamage = 0.1f; ;
        float middleOxygenDamage = 0.6f;
        float middleHealthDamage = 0.2f; ;
        float hightOxygenDamage = 1f;
        float hightHealthDamage = 0.4f; ;

        if (Level == 1 || Level == 2)
        {
            _oxygenLosingDamage = lowOxygenDamage;
            _healthLosingDamage = lowHealthDamage;
        }

        if (Level == 3 || Level == 4 || Level == 5)
        {
            _oxygenLosingDamage = middleOxygenDamage;
            _healthLosingDamage = middleHealthDamage;
        }

        if (Level >= 6)
        {
            _oxygenLosingDamage = hightOxygenDamage;
            _healthLosingDamage = hightHealthDamage;
        }
    }

    private void TryToCollectResourseOrb()
    {
        float distanceToSpot = 3.5f;
        Ray rayToSpot = new Ray(_raycastStartPoint.transform.position, transform.forward);
        RaycastHit hit; 

        if(Physics.Raycast(rayToSpot, out hit, distanceToSpot))
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
        if(_health.Value <= 0)
        {
            Instantiate(_effectOfDying, transform.position, Quaternion.identity);
            Died?.Invoke();
            Destroy(gameObject);
        }
    }

    private void TakeEternalDamagePerTime()
    {       
        if(Time.timeScale == 1)
        {
            if (_oxygen.Value <= 0)
                SlowlyDie();
            else
                _oxygen.Losing(_oxygenLosingDamage);
        }         
    }

    private void SlowlyDie()
    {
        _health.TakeEternalDamage(_healthLosingDamage); 
        Die();
    }
}
