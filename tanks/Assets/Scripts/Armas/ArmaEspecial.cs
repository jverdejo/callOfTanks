using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaEspecial : MonoBehaviour {


    public GameObject municion;
    ControladorPersonaje controladorPersonaje;
    Transform cañon;
    float proxDisparo, tiempoEntreDisparo;
    public ParticleSystem efectoDisparo;
    public float cadencia;
    public int GastoMunicion=5;
    // Use this for initialization
    void Start () {
        proxDisparo = Time.time;
        tiempoEntreDisparo = 1 / cadencia;
        cañon = transform.Find("CañonEspecial");
        controladorPersonaje = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPersonaje>();
    }
	
	// Update is called once per frame
	void Update () {
        bool disparando = Input.GetButton("Jump");
        if (disparando && proxDisparo <= Time.time && Time.time > 0)
        {
            Debug.Log("Disparo Especial");
            proxDisparo = Time.time + tiempoEntreDisparo;
            Disparar();
        }
    }

    void Disparar()
    {
        if (controladorPersonaje.municion >= GastoMunicion)
        {
            GameObject bala;
            bala = Instantiate(municion, cañon.position, cañon.rotation);
            bala.GetComponent<Rigidbody>().AddForce(bala.GetComponent<Transform>().forward * municion.GetComponent<BalaExplosiva>().velocidadBala * 1000);
            efectoDisparo.Play();
            controladorPersonaje.ReducirMunicion(GastoMunicion);
            Destroy(bala, 5);
        }

    }


}
