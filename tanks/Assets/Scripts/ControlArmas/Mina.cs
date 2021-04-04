using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour {

    public int daño = 5000;
    public float radio;
    public GameObject explosion;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
        
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radio);

            foreach (Collider tank in hitColliders)
                HacerDaño(tank.gameObject);

            Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
            Destroy(gameObject);
        }
        else
        {
            //Destroy(other.gameObject);

        }

    }

    void HacerDaño(GameObject tank)
    {
        if (tank.CompareTag("Player")){
            tank.GetComponentInParent<ControladorPersonaje>().BajarVida(daño);
        }

        else if (tank.CompareTag("Enemy")){
            tank.GetComponentInParent<ControladorEnemigo>().BajarVida(daño);
        }
        
        
    }
}