using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour {

    public GameObject jugador, municion;
    public float cadencia,alcance ;
    public ParticleSystem efectoDisparo;
    Transform cañon;
    float proxDisparo, tiempoEntreDisparo;
    public AudioSource audioSrc;


    // Use this for initialization
    void Start () {
        jugador = GameObject.FindGameObjectWithTag("Player");
        cañon = transform.Find("Cañon");
        proxDisparo = Time.time;
        tiempoEntreDisparo = 1 / cadencia;
        audioSrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!jugador)
            return;
        float distancia = (jugador.transform.position - this.transform.position).magnitude;
        if (distancia <= alcance)
        {
            if (proxDisparo <= Time.time)
            {
                Debug.Log("Disparo Enemigo");
                proxDisparo = Time.time + tiempoEntreDisparo;
                Disparar();

            }
        }
	}

    void Disparar()
    {
        audioSrc.Stop();
        audioSrc.Play();
        GameObject bala;
        bala = Instantiate(municion, cañon.position, cañon.rotation);
        bala.GetComponent<Rigidbody>().AddForce(bala.GetComponent<Transform>().forward * municion.GetComponent<Bala>().velocidadBala * 1000);
        bala.GetComponent<Bala>().enemiga = true;
        efectoDisparo.Play();
        Destroy(bala, 2);
    }
}
