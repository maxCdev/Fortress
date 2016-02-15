using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SelectBarController : MonoBehaviour {

	// Use this for initialization
    public UnitSelector selector;
	void Start () {
	
	}
    public void Drag()
    {
        Debug.Log("Drag!!!");
        if (selector.addSelectButtonDown)
        {

            //if (Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).x==
           
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
