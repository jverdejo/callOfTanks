using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaSelector : MonoBehaviour {

    GameObject controlador;
    public bool Activado = true;
    // Use this for initialization
    void Start () {
        controlador = GameObject.FindGameObjectWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Activado == true)
        {
            Debug.Log("Entrando Zona seleccion");
            controlador.GetComponentInChildren<ControladorUI>().MensajeIrTienda(true);
            controlador.GetComponentInChildren<ControladorBotones>().zonaSelector = true;
          
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && Activado == true)
        {
            Debug.Log("Saliendo Zona seleccion");
            controlador.GetComponentInChildren<ControladorUI>().MensajeIrTienda(false);
            controlador.GetComponentInChildren<ControladorBotones>().zonaSelector = false;
        }
    }

    public void ActivarZona()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Activado = true;
    }

    public void DesactivarZona()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Activado = false;
        controlador.GetComponentInChildren<ControladorUI>().MensajeIrTienda(false);
        controlador.GetComponentInChildren<ControladorBotones>().zonaSelector = false;
    }


}
