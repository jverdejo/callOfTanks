using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaExplosiva : MonoBehaviour {

    public int daño = 5000;
    public float radio;
    public GameObject explosion;
    public float velocidadBala;

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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radio);

            foreach (Collider tank in hitColliders)
                HacerDaño(tank.gameObject);
        }

       else if (other.CompareTag("Municion") || other.CompareTag("Utilidad"))
        {

        }
        Destroy(Instantiate(explosion,transform.position,transform.rotation),2);
        Destroy(gameObject);
            

    }

    void HacerDaño(GameObject tank)
    {
        if (tank.CompareTag("Player"))
        {
            tank.GetComponentInParent<ControladorPersonaje>().BajarVida(daño);
        }

        else if (tank.CompareTag("Enemy"))
        {
            tank.GetComponentInParent<ControladorEnemigo>().BajarVida(daño);
        }


    }
}
