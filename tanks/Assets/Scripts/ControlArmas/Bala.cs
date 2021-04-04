using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
    public GameObject particula;
    public float velocidadBala;
    public bool enemiga=false;
    public int daño;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!enemiga)
                return;
            Debug.Log("Impacto con el jugador: " + other.name + "  Daño hecho: " + daño);
            other.gameObject.GetComponent<ControladorPersonaje>().BajarVida(daño);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            if (enemiga)
                return;
            Debug.Log("Impacto con el enemigo: " + other.name + "  Daño hecho: " + daño);
            other.gameObject.GetComponent<ControladorEnemigo>().BajarVida(daño);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Utilidad") || other.CompareTag("Municion"))
        {

        }


        else
        {
            Debug.Log("Impacto con: " + other.name);
            Destroy(this.gameObject);
        }
        
        //Destroy(other.gameObject);

    }
    public void SetDaño(int daño)
    {
        this.daño = daño;
    }

   
}
