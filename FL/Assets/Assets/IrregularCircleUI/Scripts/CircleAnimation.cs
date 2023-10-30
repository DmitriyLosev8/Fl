using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimation : MonoBehaviour {

    public GameObject[] animCircles;
	private int _red = 0;
	private int _yellow = 1;
	private int _green = 2;
	private int _empty = 3;
	private Transform _target;
	private float _distance;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Key key))
		{
			_target = key.transform;
		}
	}


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		//foreach(GameObject go in animObjects)
		//      {
		//          Vector3 angle = go.transform.eulerAngles;

		//          angle.z += Time.deltaTime * 50f;

		//          go.transform.eulerAngles = angle;
		//      }

		Renderer();

	}

	private void Renderer()
    {
        if (_target != null)
        {
            _distance = Vector3.Distance(transform.position, _target.position);

			int circleToRender = DetermineCircle(_distance);

				Vector3 angle = animCircles[circleToRender].transform.eulerAngles;
                angle.z += Time.deltaTime * 50f;
                animCircles[circleToRender].transform.eulerAngles = angle;
        }
    }

	private int DetermineCircle(float distance)
    {
		float redDistance = 6;
		float yellowDistance = 4;
		float greenDistance = 2;

		if (_distance <= redDistance && _distance > yellowDistance)
			return _red;

		if (_distance <= yellowDistance && _distance > greenDistance)
			return _yellow;

		if (_distance <= greenDistance)
			return _green;

		return _empty;
	}
}
