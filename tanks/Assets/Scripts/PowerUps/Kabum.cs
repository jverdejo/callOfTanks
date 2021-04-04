using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kabum : MonoBehaviour {

    

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == "Player") {
            foreach (GameObject tank in GameObject.FindGameObjectsWithTag("Enemy"))
                if(tank.GetComponent<ControladorEnemigo>())
                    tank.GetComponent<ControladorEnemigo>().Muerte();

            Destroy(this.gameObject);
        }
    }
}
