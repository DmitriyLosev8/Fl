using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{  
    private float _rotateValue = - 0.5f;
    
    private void Update()
    {
        transform.Rotate(0, 0, _rotateValue);
    }
}
