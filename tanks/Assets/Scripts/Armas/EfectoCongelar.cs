using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoCongelar : MonoBehaviour {

    public float cuantoReducir = 0.5f;
    public float duracion = 5f;
    public Color color;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<MovimientoEnemigo>().AumentarVelocidad(cuantoReducir, duracion);
            other.gameObject.GetComponent<ControladorEnemigo>().CambiarColor(color, duracion);
            Destroy(this.gameObject);
        }
        else
        {

        }    
        //Destroy(other.gameObject);

    }
}
