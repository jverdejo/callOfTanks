using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour {


    public Text puntuacionGlobal, bajasGlobal, tiempoGlobal,balasGlobal,rondaGlobal;
    public GameObject panelMenu, panelMarcadores;
    // Use this for initialization
    void Start () {
		
	}
	


    public void Jugar()
    {
        SceneManager.LoadScene("Main");
    }

    public void MostrarMarcadores()
    {
        panelMenu.SetActive(false);
        panelMarcadores.SetActive(true);
        ActualizaPanelGlobal();
    }

    void ActualizaPanelGlobal()
    {
        int puntuacionTotal, muertesTotal;
        float tiempoTotal;
        int minutos, segundos;
        if (!PlayerPrefs.HasKey("PuntuacionTotal"))
        {
            PlayerPrefs.SetFloat("TiempoTotal", 0);
            PlayerPrefs.SetInt("PuntuacionTotal", 0);
            PlayerPrefs.SetInt("MuertesTotal", 0);
            PlayerPrefs.SetInt("BalasTotal", 0);
            PlayerPrefs.SetInt("RondaMaxima", 0);
        }
        /*Se calculas los totales sumando lo guardado y lo de partida*/
        puntuacionTotal = PlayerPrefs.GetInt("PuntuacionTotal") ;
        muertesTotal = PlayerPrefs.GetInt("MuertesTotal");
        tiempoTotal = PlayerPrefs.GetFloat("TiempoTotal");
        balasGlobal.text = PlayerPrefs.GetInt("BalasTotal")+"";
        rondaGlobal.text= PlayerPrefs.GetInt("RondaMaxima")+"";
        minutos = (int)(tiempoTotal / 60f);
        segundos = (int)(tiempoTotal - (minutos * 60));
        puntuacionGlobal.text = puntuacionTotal + "";
        bajasGlobal.text = muertesTotal + "";
        if (segundos < 10)
            tiempoGlobal.text = minutos + ":0" + segundos;
        else
            tiempoGlobal.text = minutos + ":" + segundos;
  

    }

    public void VolverMenu()
    {
        panelMenu.SetActive(true);
        panelMarcadores.SetActive(false);
    } 

    public void Salir()
    {
        Application.Quit();
    }

}
