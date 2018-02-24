using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{

    private bool _beep = false;
    public AudioSource BeepSource;
    private float _cooldown = 2;
    private float _cooldownCounter = 0;

    private List<Scannable> _scannedGoods;


	// Use this for initialization
	void Start ()
	{
	    _scannedGoods = new List<Scannable>();
	    BeepSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_beep)
	    {
	        if (BeepSource != null)
	        {
	            BeepSource.Play();
	        }
	        _beep = false;
	        
	    }

	    _cooldownCounter -= Time.deltaTime;
	}

    public bool IsReadyToScan()
    {
        return _cooldownCounter <= 0;
    }

    public void Scan(Scannable scanned)
    {
        _cooldownCounter = _cooldown + Random.Range(-1.5f,0.5f);
        Debug.Log(scanned.GetName());
        _scannedGoods.Add(scanned);
        _beep = true;
    }

    public List<Scannable> GetScannedGoods()
    {
        return _scannedGoods;
    }

}
