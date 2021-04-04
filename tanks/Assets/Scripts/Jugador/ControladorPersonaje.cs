using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPersonaje : MonoBehaviour {

    [Header("--Maximas:")]
    public float vidaMaxima;
    public int municionmaxima;
    public float combustibleMaximo;
    public float consumo;
    [Header("--Actuales:")]
    public float combustible;
    public float vida;
    public int municion;
    public int dinero;
    public bool sinCombustible;
    public int disparos;
    [Header("--Controlador:")]
    public GameObject Controlador;
    public ControladorUI ControladorUI;
    public GameObject particulasExplosion,restos;
    

    // Use this for initialization
    void Start () {
        vida = vidaMaxima;
        combustible = combustibleMaximo;
        municion = municionmaxima;
        dinero = 500;
        ControladorUI = Controlador.GetComponent<ControladorUI>();
        ControladorUI.IniciarUI((int)combustible, municion, dinero, vida);
	}
	
	// Update is called once per frame
	void Update () {
        if (combustible >= 0)
            combustible -= Time.deltaTime * consumo;
        if (vida <= vidaMaxima)
            vida += Time.deltaTime;
        ActualizaUI();
	}

    void ActualizaUI()
    {
        int com;
        if (combustible < 0)
        {
            com = 0;
            sinCombustible = true;
        } 
        else{
            com = (int)combustible;
            sinCombustible = false;
        }
           
        
        ControladorUI.ActualizarUI(com, municion, dinero, vida);
               

    }
    public void ReducirMunicion(int cant)
    {
        municion -= cant;
        if (municion < 0)
            municion = 0;
    }

    public void AumentarMunicion(int cant)
    {
        municion += cant;
        if (municion > municionmaxima)
            municion = municionmaxima;
    }

    public void MunicionMaxima()
    {
        municion = municionmaxima;
    }

    public void AumentarCombustible(int cant)
    {
        combustible += cant;
        if (combustible > combustibleMaximo)
            combustible = combustibleMaximo;
    }

    public void CombustibleMaximo()
    {
        combustible = combustibleMaximo;
    }

    public void BajarVida(int cant)
    {
        vida -= cant;
        if (vida <= 0)
        {
            Muerte();
        }

    }
    void Muerte()
    {
        Instantiate(particulasExplosion, transform.position, particulasExplosion.transform.rotation);
        Instantiate(restos, transform.position, restos.transform.rotation);
        Controlador.GetComponent<ControladorJuego>().FinDelJuego();

        Destroy(gameObject);
    }
    public void AumentarVida(int cant)
    {
        vida += cant;
        if (vida > vidaMaxima)
            vida = vidaMaxima;
    }

    public void AumentarDinero(int cant)
    {
        dinero += cant;
    }

    public bool Comprar(int precio)
    {
        if (precio <= dinero)
        {
            dinero -= precio;
            return true;
        }
        else
        {
            Debug.Log("No hay dinero suficiente");
            return false;
        }
            
    }

    public void AumentarPuntos(int puntos)
    {
        this.dinero += puntos;
        ControladorUI.MonstrarPuntosSumados(puntos);
        
    }

}
