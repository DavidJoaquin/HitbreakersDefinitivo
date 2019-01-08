using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocidad_Suelo : MonoBehaviour {

    Vector3 v3_posicion_anterior = Vector3.zero;

    public Vector3 v3_velocidad = Vector3.zero;

    private void Start()
    {
        v3_posicion_anterior = transform.position;
    }

    void Update()
    {
        v3_velocidad = (transform.position - v3_posicion_anterior) / Time.deltaTime;
        v3_posicion_anterior = transform.position;
    }
}
