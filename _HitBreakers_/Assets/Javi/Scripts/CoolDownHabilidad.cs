using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CoolDownHabilidad : NetworkBehaviour {

    public string boton = "Habilidad1";
  //  public Image iconoApagado;
  //  public Text textoCoolDown;

    [SerializeField] private Habilidad habilidad;
    [SerializeField] private GameObject arma;

    private Image IconoHabilidad;
    private AudioSource sonidoHabilidad;
    private float duracionCoolDown;
    private float nuevaDisponibilidad;
    private float cdRestante;

    // Use this for initialization
    void Start () {
        Inicializar(habilidad, arma);
       
    }

    public void Inicializar(Habilidad habilidadSeleccionada, GameObject elArma) {
        habilidad = habilidadSeleccionada;
        arma = elArma;
        duracionCoolDown = habilidad.coolDownBase;
        habilidad.Inicializa(elArma);
        HabilidadLista();
    }

    private void HabilidadLista()
    {
   //    textoCoolDown.enabled = false;
   //    iconoApagado.enabled = false;
    }

    private void CoolDown()
    {
        cdRestante -= Time.deltaTime;
        float roundedCd = Mathf.Round(cdRestante);
 //       textoCoolDown.text = roundedCd.ToString();
 //       iconoApagado.fillAmount = (cdRestante / duracionCoolDown);
    }

    private void BotonPulsado()
    {
        nuevaDisponibilidad = duracionCoolDown + Time.time;
        cdRestante = duracionCoolDown;
    //    iconoApagado.enabled = true;
    //    textoCoolDown.enabled = true;

      //  sonidoHabilidad.clip = habilidad.unSonido;
      //  sonidoHabilidad.Play();
        habilidad.Ejecuta();
    }

    // Update is called once per frame
    void Update () {


        if (isLocalPlayer)
        {
            bool cdCompletado = (Time.time > nuevaDisponibilidad);
            if (cdCompletado)
            {
                HabilidadLista();
                if (Input.GetButtonDown(boton))
                {
                    Debug.Log("boton pulsado");
                    BotonPulsado();
                }
            }
            else
            {
                CoolDown();
            }
        }
        

    }
}
