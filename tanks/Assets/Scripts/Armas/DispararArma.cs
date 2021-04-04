using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararArma : MonoBehaviour {
   
    public bool apuntar=true, animacion=false;
    public float cadencia;
    public GameObject municion;
    public ParticleSystem efectoDisparo;
    ControladorPersonaje controladorPersonaje;

    bool bajaInstantanea = false;
    bool disparando;

    Transform cañon;
    float proxDisparo,tiempoEntreDisparo;
    Animator animatorCamara;
    public Animator animatorDisparo;
    public AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        proxDisparo = Time.time;
        tiempoEntreDisparo = 1 / cadencia;
        cañon = transform.Find("Cañon");
        animatorCamara = Camera.main.gameObject.GetComponent<Animator>();
        controladorPersonaje = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPersonaje>();
        audioSrc = GetComponent<AudioSource>();
        
    }  
    private void OnEnable()
    {
        efectoDisparo = GetComponentInChildren<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        disparando = Input.GetButton("Fire1");

        
        if (disparando && proxDisparo <= Time.time && Time.timeScale > 0)
        {
            if (animacion)
                animatorDisparo.Play("Activado");
           
            proxDisparo = Time.time + tiempoEntreDisparo;
            Disparar();

        }
        
        if (apuntar)
        {
            animatorCamara.SetBool("apuntando", Input.GetButton("Fire2"));
        }

    }

    void Disparar()
    {
        if (controladorPersonaje.municion > 0)
        {
            audioSrc.Stop();
            audioSrc.Play();
            GameObject bala;
            bala = Instantiate(municion, cañon.position, cañon.rotation);
            bala.transform.Rotate(new Vector3(90, 0, 0));
            if (bajaInstantanea)
                bala.GetComponent<Bala>().SetDaño(10000);
            bala.GetComponent<Rigidbody>().AddForce(bala.GetComponent<Transform>().up * municion.GetComponent<Bala>().velocidadBala * 1000);
            efectoDisparo.Play();
            controladorPersonaje.ReducirMunicion(1);
            Destroy(bala, 2);
            controladorPersonaje.disparos++;
        }

    }

    public void EnBajaInstantanea(float duracion)
    {
        StartCoroutine(Activar(duracion));
    }


    IEnumerator Activar( float duracion)
    {
        bajaInstantanea = true;
        controladorPersonaje.ControladorUI.ActivarImagenCalavera(true);
        yield return new WaitForSeconds(duracion);
        bajaInstantanea = false;
        controladorPersonaje.ControladorUI.ActivarImagenCalavera(false);
    }


}
