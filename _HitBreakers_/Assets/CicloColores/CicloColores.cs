using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloColores : MonoBehaviour {

    public Material matt;

    [SerializeField]
    Gradient gradient;
    [SerializeField]
    float duration;
    float t = 0f;
    private float tiempoObjetivo;
    private bool lerpingColores;

    private float lerpIni;

    private float value;
    private Color color;

    private void Awake()
    {
        lerpIni = 0f;
    }


    void Update()
    {

        if (value == 1)
        {
            value = 0;
            color = gradient.Evaluate(0);
            matt.SetColor("_EmissionColor", color);
            t = 0;
        }
        Debug.Log(value);

            
            value = Mathf.Lerp(lerpIni, 1f, t);
            t += Time.deltaTime / duration;
            color = gradient.Evaluate(value);
            matt.SetColor("_EmissionColor", color);
            
    }

    
}
