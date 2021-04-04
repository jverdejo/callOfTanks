using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorBotones : MonoBehaviour {
    bool enSelector = false;
    bool enPausa = false;
      
    GameObject jugador;
    List<GameObject> cañones;
    public int cañonActual = 0;
    public int cañonAux = 0;
    Vector3 posicionJugador;
    
    public Transform posicionSelector;
    public GameObject plataformaSelector;

    public GameObject UIseleccion, panelMensajes;
    public GameObject UImenuPausa;

    ControladorUI ControladorUI;
    public bool zonaSelector = false;
    
    // Use this for initialization
    void Start () {
        jugador = GameObject.FindWithTag("Player");
        cañones = new List<GameObject>();
        foreach (Transform hijo in jugador.transform.Find("Cuerpo"))
        {
            cañones.Add(hijo.gameObject);
        }
       
        foreach (GameObject cañon in cañones)
        {
            cañon.SetActive(false);
        }
        cañones[cañonActual].SetActive(true);
        plataformaSelector.SetActive(false);
        UIseleccion.SetActive(false);
        ControladorUI = this.GetComponent<ControladorUI>();

    }

    // Update is called once per frame
    private void Update()
    {
      
            if (Input.GetKeyDown(KeyCode.E) && enPausa == false)
            {
                if (!enSelector)
                {
                    if (zonaSelector)
                    {
                        enSelector = true;
                        IrSelector();
                    }


                }
                else
                {
                    enSelector = false;
                    SalirSelector();

                }
            }
        
        if (enSelector && enPausa == false)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Siguiente");
                CañonSiguiente();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Anterior");
                CañonAnterior();
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && enSelector == false)
        {
            if (!enPausa)
            {
                enPausa = true;
                ActivarPausa();
            }
            else
            {
                enPausa = false;
                DesactivarPausa();
            }
        }
    }
    private void IrSelector()
    {
        cañonAux = cañonActual;

        Time.timeScale = 0;
        panelMensajes.SetActive(false);
        posicionJugador = jugador.transform.position;
        jugador.transform.position = posicionSelector.position;
        jugador.transform.rotation = Quaternion.identity;
        jugador.transform.Find("Cuerpo").rotation = Quaternion.identity;

        plataformaSelector.SetActive(true);
        jugador.GetComponent<MovimientoPersonaje>().enabled = false;
        Cursor.visible = true;
        UIseleccion.SetActive(true);
        ActualizarTextosTienda();

    }

    private void SalirSelector()
    {
        cañones[cañonActual].SetActive(false);
        cañones[cañonAux].SetActive(true);
        cañonActual = cañonAux;

        Time.timeScale = 1;
        panelMensajes.SetActive(true);
        jugador.transform.position = posicionJugador;
        plataformaSelector.SetActive(false);

        jugador.GetComponent<MovimientoPersonaje>().enabled = true;
        Cursor.visible = false;
        UIseleccion.SetActive(false);
        ActualizarTextoTorreta();
    }
    void CañonSiguiente()
    {
        cañones[cañonActual].SetActive(false);
        cañonActual++;
        cañonActual %= (cañones.Count);
        cañones[cañonActual].SetActive(true);
        ActualizarTextosTienda();
    }

    void CañonAnterior()
    {
        cañones[cañonActual].SetActive(false);
        cañonActual += (cañones.Count - 1);
        cañonActual %= (cañones.Count);
        cañones[cañonActual].SetActive(true);
        ActualizarTextosTienda();
    }

    void ActualizarTextoTorreta()
    {
        ControladorUI.ActualizarTorreta(cañones[cañonActual].GetComponent<Torreta>().Nombre, cañones[cañonActual].GetComponent<Torreta>().ArmaSecundaria); 
    }

    
    public void ActivarPausa()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        UImenuPausa.SetActive(true);
    }

    public void DesactivarPausa()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        UImenuPausa.SetActive(false);
    }

    public void Repostar(int seleccion)
    {
        if (seleccion == 0)/*medio deposito*/
        {
            if (jugador.GetComponent<ControladorPersonaje>().Comprar(300))
            {
                jugador.GetComponent<ControladorPersonaje>().AumentarCombustible((int)jugador.GetComponent<ControladorPersonaje>().combustibleMaximo / 2);
            }

        }else if (seleccion == 1)/*deposito completo*/
        {
            if (jugador.GetComponent<ControladorPersonaje>().Comprar(500))
            {
                jugador.GetComponent<ControladorPersonaje>().AumentarCombustible((int)jugador.GetComponent<ControladorPersonaje>().combustibleMaximo);
            }
        }
    }

    public void RellenarMunicion()
    {
        if (jugador.GetComponent<ControladorPersonaje>().Comprar(1100))
        {
            jugador.GetComponent<ControladorPersonaje>().AumentarMunicion((int)jugador.GetComponent<ControladorPersonaje>().municionmaxima);
        }
    }

    public void Comprar()
    {
        if (cañonActual == cañonAux)
            return;
        else if (cañones[cañonActual].GetComponent<Torreta>().Comprado)
            cañonAux = cañonActual;
        else
        {
            if (jugador.GetComponent<ControladorPersonaje>().Comprar(cañones[cañonActual].GetComponent<Torreta>().Precio))
            {
                cañones[cañonActual].GetComponent<Torreta>().Comprado = true;
                cañonAux = cañonActual;
            }
        }
        ActualizarTextosTienda();
    } 

    void ActualizarTextosTienda()
    {
        var TextoPrincipal = cañones[cañonActual].GetComponent<Torreta>().Nombre;
        var TextoSecundaria = "Secundaria: " + cañones[cañonActual].GetComponent<Torreta>().ArmaSecundaria;
        var TextoBoton = "";
        if (cañonActual == cañonAux)
            TextoBoton = "En Uso";
        else if (cañones[cañonActual].GetComponent<Torreta>().Comprado)
            TextoBoton = "Usar";
        else
            TextoBoton = "Comprar [Coste: " + cañones[cañonActual].GetComponent<Torreta>().Precio + "]";
        ControladorUI.TextosTienda(TextoPrincipal, TextoSecundaria, TextoBoton);
    }
    
}

