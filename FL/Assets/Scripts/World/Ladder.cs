using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject _spotToStart;
   
    public GameObject SpotToStart => _spotToStart;
}
