using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour {
    public float velocidad, velRotacion, aumento = 1,distanciaDeteccion=20,distanciaDetencion=10,DistanciaMarchaAtras=5;
    float inputH, inputV ,distancia;
    Rigidbody rig;
    public GameObject jugador;
    Transform torreta;


    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Transform>().gameObject;
        torreta = transform.Find("Cuerpo");

	}
	
	// Update is called once per frame
	void Update () {
        if (!jugador)
            return;
        CalcularInputH();
        Apuntar();
    }


    private void FixedUpdate()
    {

        Move();
        Turn();

    }

    private void CalcularInputH()
    {
       
        distancia = (jugador.transform.position - torreta.position).magnitude;
        if (distancia <= DistanciaMarchaAtras)/*Si el jugador esta MUY cerca el enemigo da marcha atras*/
        {
            inputV = -1f;
            if (torreta.localRotation.y < 0)
            {
                inputH = -1;
            }
            else if (torreta.localRotation.y > 0)
            {
                inputH = 1;
            }
            else
            {
                inputH = 0;
            }
        }
        else if(distancia<=distanciaDetencion)/*Si el jugador esta cerca se detiene*/
        {
            inputH = 0;
            inputV = 0;
        }
        else if(distancia<=distanciaDeteccion)/*Si el jugador esta lo suficientemente cerca para estar detectado y no esta pegado al jugador va hacia el*/
        {
            inputV = 1f;
            if (torreta.localRotation.y < 0)
            {
                inputH = -1;
            }
            else if (torreta.localRotation.y > 0)
            {
                inputH = 1;
            }
            else
            {
                inputH = 0;
            }
        }
        else/*No se detencta al jugador y se queda dando vueltas*/
        {
            inputH = 1;
            inputV = -1;
        }


    }

    private void Apuntar()
    {
     
        Vector3 rot = jugador.transform.position - torreta.position;
        Quaternion q= Quaternion.LookRotation(rot);

        q.x = 0;
        q.z = 0;
        torreta.rotation = q;


        return;
    }

    private void Move()
    {

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * inputV * velocidad *aumento * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rig.MovePosition(rig.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = inputH * velRotacion * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rig.MoveRotation(rig.rotation * turnRotation);
    }

    public void AumentarVelocidad(float cuantoAumentar, float duracion)
    {
        StartCoroutine(Aumento(cuantoAumentar, duracion));
    }


    IEnumerator Aumento(float cuantoAumentar, float duracion)
    {
        aumento = cuantoAumentar;
        yield return new WaitForSeconds(duracion);
        aumento = 1;
    }

    public void SetJugador(GameObject jug)
    {
        this.jugador = jug;
    }


}
