using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Estados))]

public class Habilidades : NetworkBehaviour
{

    public GameObject go_proyectil;
    PlayerController player;
    public Vector3 v3_origen_balas;
    Estados estados;


    // Use this for initialization
    void Start()
    {
        if (!isServer)
            return;
        player = GetComponent<PlayerController>();
        estados = GetComponent<Estados>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;
        //poner fuente de los disparos aquí en público
        if (estados.b_Disparo)
        {
            GameObject go_instancia_proyectil = (GameObject)Instantiate(go_proyectil,
                v3_origen_balas, transform.rotation);
            //Poner aquí condiciones de spawn, mirar como cogerlo según el personaje escogido para que
            //se cargue directamente las condiciones, aquí ejecutaremos las habilidades cuando sean de tipo proyectil
            //Las que sean de tipo transform se harán en PlayerController
            NetworkServer.Spawn(go_instancia_proyectil);
        }

        if (estados.b_Habilidad1)
        {
           
        }

        if (estados.b_Habilidad2)
        {
            
        }

        if (estados.b_Habilidad3)
        {
            
        }

 

        if (estados.b_Definitiva)
        {
            
        }
        /* Preparado para añadir recarga
        if (estados.b_Recarga)
        {
           
        }*/
    }
}
