using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Conexiones : MonoBehaviour {

    public GameObject go_Administrador_Red;

    // Use this for initialization
    public void Arranca_Servidor() {

        GameObject go_Admin_Red = Instantiate(go_Administrador_Red);
        go_Admin_Red.GetComponent<Administrador_Red>().Iniciar(true);
        go_Admin_Red.GetComponent<Descubre_Servidor>().Arranca_Servidor();
        Destroy(gameObject);
    }

    public void Arranca_Cliente()
    {
        GameObject go_Admin_Red = Instantiate(go_Administrador_Red);
        go_Admin_Red.GetComponent<Descubre_Servidor>().Arranca_Cliente();
        Destroy(gameObject);
    }

}
