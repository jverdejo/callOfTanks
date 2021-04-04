using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour {


    public float velocidad;
    public float velRotacion;
    
    float aumento;
    
    Vector3 movimiento;
    Rigidbody rig;
    public float camRayLength=100f;
    int floorMask;
    public GameObject cuerpo,canvasApuntado;
    float inputH, inputV;
    ControladorPersonaje controladorPersonaje;
    float multiplicadorCombustible=1;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Suelo");
        Cursor.visible = false;
        controladorPersonaje = GetComponent<ControladorPersonaje>();
        aumento = 1;
    }
    // Update is called once per frame
    void Update () {
        
    }

    private void FixedUpdate()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
        if (controladorPersonaje.sinCombustible)
        {
            multiplicadorCombustible = 0.75f;
        }
        else
        {
            multiplicadorCombustible = 1;
        }
        Move();
        Turn();

        Apuntar();
    }

    /* private void Movimiento(float inputH, float inputV)
     {
         movimiento.Set(inputH,0f,inputV);

         movimiento = movimiento*velocidad*Time.deltaTime;
         rig.MovePosition(transform.position + movimiento);
         if (movimiento != Vector3.zero)
         {
             //Vector3 suave = Vector3.Lerp(cadera.transform.forward, movimiento, velRotacion);
             cadera.transform.rotation = Quaternion.LookRotation(movimiento);
         }


     }*/


    private void Move()
    {
        
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * inputV * velocidad * aumento * Time.deltaTime*multiplicadorCombustible;
        
        // Apply this movement to the rigidbody's position.
        rig.MovePosition(rig.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn =  inputH* velRotacion * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rig.MoveRotation(rig.rotation * turnRotation);
    }
    void Apuntar()
    {
        Ray CamRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        RaycastHit hitSuelo;
       
        if (Physics.Raycast(CamRay, out hitSuelo, camRayLength, floorMask))
        {
            Vector3  PlayerToMouse = hitSuelo.point - transform.position;
            PlayerToMouse.y = 0;
            
            Quaternion rot = Quaternion.LookRotation(PlayerToMouse);
            cuerpo.transform.rotation = rot;

            Vector3 posCanvasAp = hitSuelo.point;
            posCanvasAp.y = 0;
            canvasApuntado.transform.position = posCanvasAp;
           
        }
    }

    public void AumentarVelocidad(float cuantoAumentar, float duracion)
    {
        StartCoroutine(Aumento(cuantoAumentar, duracion));
    }
   

    IEnumerator Aumento(float cuantoAumentar, float duracion)
    {
        aumento = cuantoAumentar;
        controladorPersonaje.ControladorUI.ActivarImagenVelocidad(true);
        yield return new WaitForSeconds(duracion);
        aumento = 1;
        controladorPersonaje.ControladorUI.ActivarImagenVelocidad(false);
    }



}
