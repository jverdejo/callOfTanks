using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanqueSeñuelo : MonoBehaviour
{

    public float velocidad, velRotacion, tiempoHastaExplotar;
    public int daño, radio;
    Rigidbody rig;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        Invoke("Explotar", tiempoHastaExplotar);

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * velocidad * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rig.MovePosition(rig.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = velRotacion * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rig.MoveRotation(rig.rotation * turnRotation);
    }

    private void Explotar()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radio);

        foreach (Collider tank in hitColliders)
            HacerDaño(tank.gameObject);

        foreach (GameObject tank in GameObject.FindGameObjectsWithTag("Enemy"))
            if (tank.GetComponent<ControladorEnemigo>())
                tank.GetComponent<MovimientoEnemigo>().SetJugador(GameObject.FindGameObjectWithTag("Player"));

        Destroy(Instantiate(explosion, transform.position, transform.rotation), 2);
        Destroy(gameObject);

    }

   

    void HacerDaño(GameObject tank)
    {
        if (tank.CompareTag("Enemy"))
        {
            tank.GetComponentInParent<ControladorEnemigo>().BajarVida(daño);
        }

    }

}
