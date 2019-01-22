using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Seleccion_Personaje : MonoBehaviour {



    public Button btn_Goku;
    public Button btn_Minion;


	public void Desactiva_Boton_Goku()
    {
        btn_Goku.interactable = false;
    }

    public void Desactiva_Boton_Minion()
    {
        btn_Minion.interactable = false;
    }

    public void Quiero_A_Goku()
    {
        GameObject.Find("Administrador_Red(Clone)").GetComponent<Administrador_Red>().Client_Envia_Seleccion_Personaje(1);
    }

    public void Quiero_A_Minion()
    {
        GameObject.Find("Administrador_Red(Clone)").GetComponent<Administrador_Red>().Client_Envia_Seleccion_Personaje(2);
    }









}
