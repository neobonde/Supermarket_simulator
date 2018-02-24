using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private Dictionary<int, Collision> _goodsOnConveyor;
    private ConveyorLaser _laser;

    private Vector3 _speed;
    private Vector3 _maxSpeed = (Vector3.back * 0.25f);
    private float _slowDownCounter;
    private float _accelerationFactor = 2;

    // Use this for initialization
    void Start ()
    {
        _slowDownCounter = 0f;
        _goodsOnConveyor = new Dictionary<int, Collision>();
        _laser = GetComponentInChildren<ConveyorLaser>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

	    if (_laser.Intersected)
	    {
	        _speed = Vector3.Lerp(_maxSpeed, Vector3.zero,_slowDownCounter);
	        _slowDownCounter = Mathf.Clamp(_slowDownCounter + Time.fixedDeltaTime * _accelerationFactor, 0f, 1f);
        }
	    else
	    {
	        _speed = Vector3.Lerp(_maxSpeed, Vector3.zero, _slowDownCounter);
	        _slowDownCounter = Mathf.Clamp(_slowDownCounter - Time.fixedDeltaTime * _accelerationFactor,0f,1f);
            
	    }

        foreach (Collision good in _goodsOnConveyor.Values)
	    {
	        good.rigidbody.velocity = _speed;

          
	    }
	    
	}


   
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            _goodsOnConveyor.Add(other.gameObject.GetInstanceID(), other);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {

            other.rigidbody.velocity = Vector3.zero;
            _goodsOnConveyor.Remove(other.gameObject.GetInstanceID());
            
        }
    }

    void StopMovement(Collision col)
    {
        col.rigidbody.velocity = Vector3.zero;
    }

}
