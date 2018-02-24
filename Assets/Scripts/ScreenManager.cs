using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

    public TextMeshPro ProductList;
    public TextMeshPro PriceList;
    public TextMeshPro TotalPrice;

    private float _totalAmount;

    private Scanner _scanner;

	// Use this for initialization
	void Start ()
	{
	    _scanner = GameObject.FindGameObjectWithTag("Scanner").GetComponent<Scanner>();
	}
	
    

	// Update is called once per frame
	void Update ()
	{
	    ProductList.text = "";
	    PriceList.text = "";
	    _totalAmount = 0;

	    List<Scannable> goods = _scanner.GetScannedGoods();

        for (int i = Mathf.Max(0, goods.Count - 9); i < goods.Count; i++)
        {
            ProductList.text += goods[i].GetName() + "\n";
            PriceList.text += goods[i].GetPrice().ToString("0.00") + "\n";
        }

	    foreach (Scannable good in _scanner.GetScannedGoods())
	    {
	        _totalAmount += good.GetPrice();
	    }
	    TotalPrice.text = _totalAmount.ToString("0.00");
	}
}
