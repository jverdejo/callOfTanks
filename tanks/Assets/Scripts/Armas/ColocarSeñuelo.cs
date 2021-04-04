using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarSeñuelo : MonoBehaviour {
    GameObject player;
    public GameObject municion;

    public int GastoMunicion = 5;
    public float cadencia = 0.1f;
    float tiempoEntreDisparo;
    float proxDisparo;
    

    // Use this for initialization
    void Start () {
        proxDisparo = Time.time;
        tiempoEntreDisparo = 1 / cadencia;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        bool disparando = Input.GetButton("Jump");
        if (disparando && proxDisparo <= Time.time && Time.time > 0)
        {
            Debug.Log("Señuelo colocado");
            proxDisparo = Time.time + tiempoEntreDisparo;
            Colocar();
        }
    }

    void Colocar()
    {
        GameObject señuelo;
        if (player.GetComponent<ControladorPersonaje>().municion >= GastoMunicion)
        {
            player.GetComponent<ControladorPersonaje>().ReducirMunicion(GastoMunicion);

            señuelo = Instantiate(municion, player.GetComponent<Transform>().position, player.GetComponent<Transform>().rotation);

            foreach (GameObject tank in GameObject.FindGameObjectsWithTag("Enemy"))
                if (tank.GetComponent<ControladorEnemigo>())
                    tank.GetComponent<MovimientoEnemigo>().SetJugador(señuelo);

        }

    }
}
