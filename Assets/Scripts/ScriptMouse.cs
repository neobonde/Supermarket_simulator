using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class ScriptMouse : MonoBehaviour
{
    private Camera _camera;
    public Rigidbody Cursor;

    public Transform PlaneGo;
    private Plane _plane;

    private int _layerMask;

    private bool _mouseClicked = false;
    private int _scrollWheel = 0;
    private float _rotationCounter = 1;
    private float  _rotationGoal;
    private float _rotationStart;
    private float _rotationFactor = 5;

    private Rigidbody _grabbedObject;
    private float _grabbedObjectOldDrag;




    // Use this for initialization
    void Start ()
    {
        _grabbedObject = null;
	    _camera = GetComponent<Camera>();

	    _layerMask = LayerMask.GetMask("Grabbable");
        _plane  = new Plane(Vector3.up, PlaneGo.position);
        PlaneGo.gameObject.SetActive(false);
    }


	// Update is called once per frame
	void Update ()
	{
	    _mouseClicked = Input.GetAxis("Grab") > 0;
	    if (_mouseClicked == false)
	    {
	        if (_grabbedObject != null)
	        {

	            _grabbedObject.angularDrag = _grabbedObjectOldDrag;
	            _grabbedObject.useGravity = true;
	        }
	        _grabbedObject = null;
	    }

	    float scroll = Input.GetAxis("Mouse ScrollWheel");
	    _scrollWheel = scroll > 0 ? 1 : scroll < 0 ? -1 : 0; //Forcing axis to return either -1 0 or 1 and not a small float;


	    //ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
	}

    void FixedUpdate()
    {
        Cursor.MovePosition(GetCursorPosition());

        if (_mouseClicked && _grabbedObject == null)
        {
            GetObjectUnderCursor();
        }

        
        if (_grabbedObject != null)
        {
            Vector3 deltaPos = Cursor.position - _grabbedObject.position;
            _grabbedObject.velocity = deltaPos * Time.fixedDeltaTime*500;

            //_grabbedObject.MovePosition(Cursor.position);

            if (_scrollWheel != 0)
            {
                _rotationCounter = 0;
                _rotationStart = _grabbedObject.rotation.eulerAngles.z;
                _rotationGoal = _rotationStart + 90 * _scrollWheel;
            }
            if(_rotationCounter <= 1)
            {
                float rot = Mathf.LerpAngle(_rotationStart, _rotationGoal, _rotationCounter);
                Vector3 objectRot = _grabbedObject.rotation.eulerAngles;
                _grabbedObject.MoveRotation(Quaternion.Euler(objectRot.x,objectRot.y,rot));
                _rotationCounter += Time.fixedDeltaTime * _rotationFactor;
            }



        }

    }

    private void GetObjectUnderCursor()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 999, _layerMask))
        {
            _grabbedObject = hit.rigidbody;
            _grabbedObjectOldDrag = _grabbedObject.angularDrag;
            _grabbedObject.useGravity = false;
            _grabbedObject.angularDrag = 1000f;
            _rotationStart = _grabbedObject.rotation.eulerAngles.z;
            _rotationGoal = _rotationStart;
        }
    }
    private Vector3 GetCursorPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        float hit;

        if (_plane.Raycast(ray, out hit))
        {
            Vector3 hitPoint = ray.GetPoint(hit);

            return hitPoint;
        }
        else
        {
            return Vector3.zero;
        }
    }



}
