﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionMaxima : MonoBehaviour {

   

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<ControladorPersonaje>().MunicionMaxima();

            Destroy(this.gameObject);
        }
    }
}
