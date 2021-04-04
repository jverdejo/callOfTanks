using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorEnemigo : MonoBehaviour {  
    public int vida = 100;
    public int puntos = 50;
    ControladorJuego controlador;
    public GameObject explosion;
    ControladorPowerUpd powerUps;
    public Slider vidaSlider;
    public Color ColorOriginal;
    
    // Use this for initialization
    void Start () {
        controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJuego>();
        powerUps = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorPowerUpd>();
        vidaSlider.maxValue = vida;
        vidaSlider.gameObject.SetActive(false);
        //ColorOriginal = gameObject.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BajarVida(int cant)
    {
        if(!vidaSlider.gameObject.activeSelf)
            vidaSlider.gameObject.SetActive(true);
        vida -= cant;
        if (vida <= 0)
        {
            Muerte();
           
        }
        vidaSlider.value = vida;

    }

    public void Muerte()
    {
        controlador.EliminarEnemigo(puntos);
       
        Vector3 pos = transform.position;
        pos.y = 0;
        Destroy(Instantiate(explosion, pos, explosion.transform.rotation),3);
        powerUps.GenerarPowerUp(transform.position);
        Destroy(this.gameObject);
        
    }

    public void CambiarColor(Color color, float duracion)
    {
        foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            if (!(r is SpriteRenderer))
                r.material.color = color;
        }
           
        Invoke("RecuperarColor", duracion);
    }

    void RecuperarColor()
    {
        foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            if (!(r is SpriteRenderer))
                r.material.color = ColorOriginal;
        }
          
    }
}
