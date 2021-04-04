using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControladorJuego : MonoBehaviour {
    public int enemigosRonda=5, aumentoEnemigos=2, enemigosRestantes=0,rondaActual=0,enemigosEliminados=0,puntuacionTotal=0,disparos=0;
    public bool juegoPausado,muerto=false;
    public Transform[] generadores;
  
    public GameObject GOenemigo;
    public float tiempoEntreRondas = 10;
    bool esperaEntreRondas=false;
    public float tamañoMapa;
    public GameObject personaje;
    public GameObject pantallaFin,UI;

    ControladorUI ControladorUI;

    public GameObject[] Refinerias;
    
    // Use this for initialization
    void Start () {
        
        enemigosRonda -= aumentoEnemigos;
        esperaEntreRondas = true;
        tamañoMapa -= 10;/*No aparezca justo en el limite*/
        ControladorUI = this.GetComponent<ControladorUI>();
        Invoke("GenerarEnemigos", tiempoEntreRondas);
       
    }
	
	// Update is called once per frame
	void Update () {
        if (enemigosRestantes <= 0 && !esperaEntreRondas)
        {
            esperaEntreRondas = true;
            /*Activar zonas de compra*/
            ZonasCompra(true);
            Debug.Log("Todos los enemigos eliminados");
            Invoke("GenerarEnemigos", tiempoEntreRondas);
        }
	}

    Vector3 PoscionAleatoriaMapa()
    {
        Vector3 pos = new Vector3(Random.Range(-tamañoMapa,tamañoMapa),-1.07f,Random.Range(-tamañoMapa,tamañoMapa));
        return pos;
    }

    void GenerarEnemigos()
    {
        rondaActual++;
       
        enemigosRonda += aumentoEnemigos;

        ZonasCompra(false);

        for (int i = 0; i < enemigosRonda; i++,enemigosRestantes++)
        {
            Instantiate(GOenemigo,PoscionAleatoriaMapa(), GOenemigo.transform.rotation);
        }
        ActualizaUI();
        esperaEntreRondas = false;
    }

    void ActualizaUI()
    {
        ControladorUI.NuevaRonda(enemigosRestantes, rondaActual);

    }

   
    public void EliminarEnemigo(int puntos)
    {
        enemigosRestantes--;
        enemigosEliminados++;
        puntuacionTotal += puntos;
        ActualizaUI();
        personaje.GetComponent<ControladorPersonaje>().AumentarPuntos(puntos);

    }

    public void SalirJuego()
    {
        SceneManager.LoadScene("Menu");
    }


    public void FinDelJuego()
    {
        disparos = personaje.GetComponent<ControladorPersonaje>().disparos;
        muerto = true;
        DesactivarIA();
        UI.SetActive(false);
        pantallaFin.SetActive(true);
    }

    void DesactivarIA()
    {
        foreach (GameObject tank in GameObject.FindGameObjectsWithTag("Enemy"))
            if (tank.GetComponent<MovimientoEnemigo>())
            {
                tank.GetComponent<MovimientoEnemigo>().enabled = false;
                tank.GetComponentInChildren<DisparoEnemigo>().enabled = false;
            }
                
    }

    void ZonasCompra(bool valor)
    {
        if (valor == true)
        {
            foreach (GameObject Refineria in Refinerias)
                Refineria.GetComponent<ZonaSelector>().ActivarZona();
        }
        else
        {
            foreach (GameObject Refineria in Refinerias)
                Refineria.GetComponent<ZonaSelector>().DesactivarZona();
        }
    }
}
