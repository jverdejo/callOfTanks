using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivacionLuces : MonoBehaviour {
    public GameObject luz;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
        {  
            luz.SetActive(!luz.activeSelf);
            
        }
    }
}
