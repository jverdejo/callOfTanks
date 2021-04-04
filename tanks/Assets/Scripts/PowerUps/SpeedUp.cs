using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUp : MonoBehaviour {
    public float cuantoAumentar = 2f;
    public float duracion = 5f;

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
            other.gameObject.GetComponent<MovimientoPersonaje>().AumentarVelocidad(cuantoAumentar, duracion);
            Destroy(this.gameObject);
        }
    }

  
}
