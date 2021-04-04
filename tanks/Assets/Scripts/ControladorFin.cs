using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ControladorFin : MonoBehaviour {
    ControladorJuego controlador;
    public Text puntuacionGlobal, bajasGlobal, tiempoGlobal,rondaGlobal;
    public Text puntuacionPartida, bajasPartida, tiempoPartida,rondaPartida;
    public bool resetPrefs=false;
    public GameObject camaraMuerte;
    int rondaActual;
    
    void OnEnable()
    {
        Cursor.visible = true;
        if(resetPrefs)
            PlayerPrefs.DeleteAll();
        controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJuego>();
        camaraMuerte.transform.position = controlador.personaje.transform.position;
        camaraMuerte.SetActive(true);
        
        ActualizaPanelPartida();
        ActualizaPanelGlobal();
       
    }

    void ActualizaPanelPartida()
    {
        float tiempo = Time.timeSinceLevelLoad ;
        int minutos, segundos;
        minutos = (int)(tiempo / 60f);
        segundos = (int)(tiempo-(minutos*60));
        rondaActual = controlador.rondaActual;
        puntuacionPartida.text = controlador.puntuacionTotal+"";
        bajasPartida.text = controlador.enemigosEliminados + "";
        rondaPartida.text = rondaActual + "";
        if (segundos<10)
            tiempoPartida.text =  minutos+":0"+segundos;
        else
            tiempoPartida.text = minutos + ":" + segundos;
    }
    void ActualizaPanelGlobal()
    {
        int puntuacionTotal, muertesTotal,balasTotal,rondaMaxima;
        float tiempoTotal;
        int minutos, segundos;
        if (!PlayerPrefs.HasKey("PuntuacionTotal"))
        {
            PlayerPrefs.SetFloat("TiempoTotal",0);
            PlayerPrefs.SetInt("PuntuacionTotal", 0);
            PlayerPrefs.SetInt("MuertesTotal", 0);
            PlayerPrefs.SetInt("BalasTotal", 0);
            PlayerPrefs.SetInt("RondaMaxima", 0);
        }
        /*Se calculas los totales sumando lo guardado y lo de partida*/
        puntuacionTotal = PlayerPrefs.GetInt("PuntuacionTotal")+ controlador.puntuacionTotal;
        muertesTotal = PlayerPrefs.GetInt("MuertesTotal") + controlador.enemigosEliminados;
        tiempoTotal = PlayerPrefs.GetFloat("TiempoTotal") + Time.timeSinceLevelLoad;
        balasTotal = PlayerPrefs.GetInt("BalasTotal") + controlador.disparos;
        rondaMaxima = PlayerPrefs.GetInt("RondaMaxima");
        if (rondaActual > rondaMaxima)
        {
            rondaMaxima = rondaActual;
        }

        minutos = (int)(tiempoTotal / 60f);
        segundos = (int)(tiempoTotal - (minutos * 60));
        puntuacionGlobal.text = puntuacionTotal + "";
        bajasGlobal.text = muertesTotal + "";
        rondaGlobal.text = rondaMaxima + "(MAX)";
        if (segundos < 10)
            tiempoGlobal.text = minutos + ":0" + segundos;
        else
            tiempoGlobal.text = minutos + ":" + segundos;
        /*Guardamos los datos*/
        PlayerPrefs.SetFloat("TiempoTotal", tiempoTotal);
        PlayerPrefs.SetInt("PuntuacionTotal", puntuacionTotal);
        PlayerPrefs.SetInt("MuertesTotal", muertesTotal);
        PlayerPrefs.SetInt("BalasTotal", balasTotal);
        PlayerPrefs.SetInt("RondaMaxima", rondaMaxima);

    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Main");
    }

}
