using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BajaInstantanea : MonoBehaviour {
    public float duracion;

    
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInChildren<DispararArma>().EnBajaInstantanea(duracion);
            Destroy(this.gameObject);
        }
    }
}
