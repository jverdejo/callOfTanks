using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUI : MonoBehaviour {

    public GameObject UI;
    public Text EnemigosRestantes ;
    public Text RondaActual;
    public Slider Combustible;
    public Slider Vida;
    public Image ColorVida;
    public Text Dinero;
    public Text Municion;
    public Text Torreta ;
    public Text Secundaria;
    public GameObject SumaPuntos;
    public GameObject PosicionSumarPuntos;
    public GameObject IrTienda;

    public Text TiendaTorreta;
    public Text TiendaSecundaria;
    public Text TiendaBoton;

    public GameObject ImagenCalavera;
    public GameObject ImagenVelocidad;

    public void IniciarUI(int combustible, int municion, int dinero, float vida)
    {
        ActualizarVidaMaxima(vida);
        ActualizarCombustibleMaximo(combustible);
        ActualizarUI(combustible, municion, dinero, vida);
    }

    public void NuevaRonda(int enemigosRestantes, int ronda)
    {
        EnemigosRestantes.text = "Enemigos Restantes: " + enemigosRestantes;
        RondaActual.text = ronda.ToString();
    }

    public void ActualizarUI(int combustible, int municion, int dinero, float vida)
    {
        Combustible.value = combustible;
        Vida.value = vida;
        Municion.text = municion.ToString();
        Dinero.text = dinero.ToString();
        ActualizarColorVida(vida);
    }

    public void ActualizarVidaMaxima(float vida)
    {
        Vida.maxValue = vida;
    }

    void ActualizarColorVida(float vida)
    {
        ColorVida.color = Color.Lerp(Color.red, Color.green, (float)vida / Vida.maxValue);
    }

    public void ActualizarDinero(int dinero)
    {
        Dinero.text = dinero.ToString();
    }

    public void ActualizarTorreta(string principal, string secundaria)
    {
        Torreta.text = principal;
        Secundaria.text = secundaria;
    }

    public void ActualizarCombustible(int combustible)
    {
        Combustible.value = combustible;
    }

    public void ActualizarCombustibleMaximo(int combustible)
    {
        Combustible.maxValue = combustible;
    }

    public void MonstrarPuntosSumados(int puntos)
    {
        GameObject objeto = Instantiate(SumaPuntos, PosicionSumarPuntos.transform);
        objeto.transform.SetParent(UI.transform);
        objeto.GetComponent<Text>().text = "+ " + puntos;
        objeto.GetComponent<Animator>().Play("SumarDinero");
        Destroy(objeto, 0.5f);
    }

    public void MensajeIrTienda(bool valor)
    {
        IrTienda.SetActive(valor);
    }

    public void TextosTienda(string Principal, string Secundaria, string Boton)
    {
        TiendaTorreta.text = Principal;
        TiendaSecundaria.text = Secundaria;
        TiendaBoton.text = Boton;
    }
   
    public void ActivarImagenCalavera(bool Valor)
    {
        ImagenCalavera.SetActive(Valor);
    }

    public void ActivarImagenVelocidad(bool Valor)
    {
        ImagenVelocidad.SetActive(Valor);
    }

}
