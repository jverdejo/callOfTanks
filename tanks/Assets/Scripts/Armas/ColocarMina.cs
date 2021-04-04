using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarMina : MonoBehaviour {
    GameObject player;

    public GameObject municion;
    public int GastoMunicion = 5;
    public float cadencia = 0.1f;
    public float tiempoActivacion = 2;
    float tiempoEntreDisparo;
    float proxDisparo;
    GameObject mina;

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
            Debug.Log("Mina colocada");
            proxDisparo = Time.time + tiempoEntreDisparo;
            Colocar();
        }
    }

    void Colocar()
    {
        if (player.GetComponent<ControladorPersonaje>().municion >= GastoMunicion)
        {
            player.GetComponent<ControladorPersonaje>().ReducirMunicion(GastoMunicion);

            mina = Instantiate(municion, player.GetComponent<Transform>().position, player.GetComponent<Transform>().rotation);
            Invoke("Activar", tiempoActivacion);

        }

    }
    void Activar()
    {
        mina.GetComponent<BoxCollider>().enabled = true;
    }

}
