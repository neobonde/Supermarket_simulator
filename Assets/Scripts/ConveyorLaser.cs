using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorLaser : MonoBehaviour
{

    public bool Intersected;
    private List<Collider> _onLaser;

	// Use this for initialization
	void Start ()
	{
	    Intersected = false;
        _onLaser = new List<Collider>();
	}
	

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            _onLaser.Add(other);
            UpdateIntersected();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            _onLaser.Remove(other);
            UpdateIntersected();
        }
    }

    private void UpdateIntersected()
    {
        Intersected = _onLaser.Count > 0;
    }

}
