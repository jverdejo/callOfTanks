using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    public GameObject radar;
    public bool radarActivado;
    GameObject enemigo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!radarActivado)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            return;
        }

        /*si no hay enemigo se busca*/
        if (enemigo == null)
            BuscarEnemigo();
        /*Se comprueba si se ha encontrado*/
        if (enemigo != null)
        {

            GetComponent<SpriteRenderer>().enabled = true;

            Vector3 rot = enemigo.transform.position - radar.transform.position;
            Quaternion q = Quaternion.LookRotation(rot);

            q.x = 0;
            q.z = 0;
            radar.transform.rotation = q;


        }
        else
        {
            
            GetComponent<SpriteRenderer>().enabled = false;
        }
	}



    void BuscarEnemigo()
    {
        enemigo = GameObject.FindGameObjectWithTag("Enemy");
    }
}
