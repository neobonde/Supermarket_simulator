using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Scannable : MonoBehaviour
{
    private Transform _barcodeNormal;
    private LayerMask _layerMask;

    public float Price;
    public string ProductName;


    // Use this for initialization
    void Start ()
    {
        if (string.IsNullOrEmpty(ProductName))
	    {
	        ProductName = "Unknown";
	    }
	    _barcodeNormal = GetComponentInChildren<Transform>();
	    _layerMask = LayerMask.NameToLayer("Scanner");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    LookForScanner();

	}

    private void LookForScanner()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position,_barcodeNormal.up,Color.red);

        if (Physics.Raycast(transform.position,_barcodeNormal.up,out hit,0.3f,_layerMask))
        {
            Scanner scanner;
            scanner = hit.transform.gameObject.GetComponent<Scanner>();

                if (scanner != null)
                {
                    if (scanner.IsReadyToScan())
                        scanner.Scan(this);
                }
        }
    }

    public string GetName()
    {
        return ProductName;
    }

    public float GetPrice()
    {
        return Price;
    }

}
