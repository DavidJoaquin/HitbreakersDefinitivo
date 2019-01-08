using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//Crearemos uno para cada tipo proyectil
//lo más comodo va a ser crear los prefabs de los personajes y modificar cada proyectil a mano
//Habilidades con spawn todas creadas aquí

public class Basico : NetworkBehaviour
{

    public float f_velocidad = 20f;
    public Vector3 v3_direccion = Vector3.forward;
    public float f_tiempo_vida = 5f;

    // Use this for initialization
    void Start()
    {
        if (!isServer)
        {
            Destroy(GetComponent<Rigidbody>());
            return;
        }


        GetComponent<Rigidbody>().velocity = v3_direccion * f_velocidad;
        Destroy(gameObject, f_tiempo_vida);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
