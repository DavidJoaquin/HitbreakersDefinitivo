using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Malo : NetworkBehaviour {

    public NavMeshAgent nma_suelo; // Establece la malla transitable del malo

	// Use this for initialization
	void Start () {
        if(!isServer)
        {
            Destroy(nma_suelo);
            return;
        }

        

    }
	
	// Update is called once per frame
	void Update () {
//El objeto que buscará para seguir "atacar"
        GameObject go_goku = GameObject.Find("songoku(Clone)"); //Clone pq es una instancia del objeto

        if(go_goku !=null)
            //setDestination define un transform objetivo (.position para coger la posición del gameobject)
            nma_suelo.SetDestination(go_goku.transform.position);
    }
}
