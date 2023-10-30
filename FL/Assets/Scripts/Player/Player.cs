using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(LightContainer))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _oxygen;
    [SerializeField] private ParticleSystem _effectOfDying;
    [SerializeField] private Transform _raycastStartPoint;
    [SerializeField] private GameObject _laternSpotLight;
    [SerializeField] private Transform _keySpace;
    [SerializeField] private Arrow _arrow;

    private Light _spotLight;
    private float _positionYToSelfDesttoy = -30f;
    private float _oxygenLosingDamage; 
    private float _healthLosingDamage;
    private int _startSpotLightAngle;
    private LightContainer _lightContainer;
    
    public bool IsHaveKey { get; private set; }
    public int DefaultValueOfCurrentKeyId = 100;
    public int CurrentKeyID;
    
    public static event UnityAction Died;
   
    public event UnityAction<float> HealthChanged;
    public event UnityAction<float> OxygenChanged;

    public int Level { get; private set; }
    public float Health => _health;
    public float Oxygen => _oxygen;
    
    private void Start()
    {
        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Level))
            Level = UnityEngine.PlayerPrefs.GetInt(KeySave.Level);
           
        SetEternalDamage();
        SetStartSpotLightAngle();
    }

    private void Awake()
    {
        _lightContainer = GetComponent<LightContainer>();
        _spotLight = _laternSpotLight.GetComponent<Light>();
    }

    private void Update()
    {
        if (transform.position.y <= _positionYToSelfDesttoy)
            Die();

        TryToCollectOrb();
        EternalDamagePerTime();   
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

    private void TryToCollectOrb()
    {
        float distanceToSpot = 3.5f;
        Ray rayToSpot = new Ray(_raycastStartPoint.transform.position, transform.forward);
        RaycastHit hit; 

        if(Physics.Raycast(rayToSpot, out hit, distanceToSpot))
        {
            Orb orb = hit.collider.gameObject.GetComponent<Orb>();

            if (orb)
            {
                orb._isSpoted = true;
                orb.DeterminePlayer(this);          
            }        
        }
    }

    public void TakeHealthDamage(float healthDamage)
    {
        _health -= healthDamage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0) 
            Die();
    }

    public void TakeOxygenDamage(float oxygenDamage)
    {
        if(_oxygen > 0)
        {
            _oxygen -= oxygenDamage;
            OxygenChanged?.Invoke(_oxygen);
        }

        if (_oxygen < 0)
            _oxygen = 0;
    }

    public void TakeLightDamage(float lightDamage)
    {
        if(_spotLight.spotAngle > 0)
            _spotLight.spotAngle -= lightDamage;
    }

    public void TakeOxygen(float valueOfOxygen)
    {
        int maxOxygen = 100;

        if (_oxygen < maxOxygen)
        {
            _oxygen += valueOfOxygen;
            OxygenChanged?.Invoke(_oxygen);
        }  
    }

    public void TakeLight(float valueOfLight)
    {
        int maxValueOfLigth = 50;

        if (_spotLight.spotAngle < maxValueOfLigth)
            _spotLight.spotAngle += valueOfLight;

        CollectLightOrb();
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
            key.transform.position = _keySpace.position;
            key.transform.rotation = _keySpace.rotation;
            key.transform.SetParent(_keySpace);
            IsHaveKey = true;
            CurrentKeyID = key.Id;
            EnableArrow();
        } 
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
                DisableArrow();
                CurrentKeyID = DefaultValueOfCurrentKeyId;
                Destroy(GetComponent<Key>());
            }                
        }
    }

    private void Die()
    {
        if(_health <= 0)
        {
            Instantiate(_effectOfDying, transform.position, Quaternion.identity);
            Died?.Invoke();
            Destroy(gameObject);
        }
    }

    private void EternalDamagePerTime()
    {       
        if(Time.timeScale == 1)
        {
            if (_oxygen <= 0)
                SlowlyDie();
            else
                OxygenLosing();
        }         
    }

    private void SlowlyDie()
    {
        _health -= _healthLosingDamage * Time.deltaTime;
        HealthChanged?.Invoke(_health);
        Die();
    }

    private void OxygenLosing()
    {
        _oxygen -= _oxygenLosingDamage * Time.deltaTime;
        OxygenChanged?.Invoke(_oxygen);
    }

    private void EnableArrow()
    {
        _arrow.gameObject.SetActive(true);
    }

    private void DisableArrow()
    {
        _arrow.gameObject.SetActive(false);
    }
}
