using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPowerUpd : MonoBehaviour {
    public GameObject[] powerUps;
    public GameObject[] powerUpsEsencial;


    public int radio;
    public int tiempo;
    public int porcentajeEsencial;

    // Use this for initialization
    void Start () {
      
        //BucleGenerar();
	}

    private void Update()
    {
        
    }

    public void GenerarPowerUp(Vector3 pos)
    {
        int rand = Random.Range(0, 101);
        int tipo;
        Debug.Log("Aleatorio: " + rand);
        if (rand%3==1)
        {
            rand = Random.Range(0, 101);
            Debug.Log("Aleatorio 2: " + rand);
            if (rand <= porcentajeEsencial)/*Esenciales*/
            {
                tipo = Random.Range(0, powerUpsEsencial.Length);
                pos.y = powerUpsEsencial[tipo].transform.position.y;
                Instantiate(powerUpsEsencial[tipo], pos, powerUpsEsencial[tipo].transform.rotation);

            }
            else
            {
                tipo = Random.Range(0, powerUps.Length);
                pos.y = powerUps[tipo].transform.position.y;
                Instantiate(powerUps[tipo], pos, powerUps[tipo].transform.rotation);

            }
        }



    }

    /*Pruebas*/
    /*
    void BucleGenerar()
    {
        for (int i = 0; i < powerUps.Length; i++)
            GenerarPowerUp(i);
        Invoke("BucleGenerar", tiempo);
    }

    int RandomNegativo()
    {
        if (Random.Range((int)0, 2) == 0)
            return 1;
        else return -1;
    }


    void GenerarPowerUp(int tipo)
    {

        int x, z;
        x = Random.Range(0, radio + 1);
        z = (int)Mathf.Sqrt(radio * radio - x * x);
        x *= RandomNegativo();
        z *= RandomNegativo();


        switch (tipo)
        {
            case 0:
                if (numBajaIns >= numBajaInsMAX)
                    return;
                numBajaIns++;
                break;
            case 1:
                if (numMunicion >= numMunicionMAX)
                    return;
                numMunicion++;
                break;
            case 2:

                if (numCombustible >= numCombustibleMAX)
                    return;
                numCombustible++;
                break;
            case 3:
                if (numKabum >= numKabumMAX)
                    return;
                numKabum++;
                break;
            case 4:
                if (numSpeed >= numSpeedMAX)
                    return;
                numSpeed++;
                break;
            default:
                Debug.LogError("Tipo incorrecto");
                break;
        }


        Instantiate(powerUps[tipo], new Vector3(jugador.transform.position.x+x, 0, jugador.transform.position.z+z), powerUps[tipo].transform.rotation);
        Debug.Log("Tipo " + tipo + "pos:" + x + "1" + z);
    }

    public void EliminarPowerUp(int tipo)
    {
        switch (tipo)
        {
            case 0:
                numBajaIns--;
                break;
            case 1:
                numMunicion--;
                break;
            case 2:
                numCombustible--;
                break;
            case 3:
                numKabum--;
                break;
            case 4:
                numSpeed--;
                break;
            default:
                Debug.LogError("Tipo incorrecto");
                break;
        }
    }*/

}
