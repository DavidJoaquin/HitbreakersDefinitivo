using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Habilidad : ScriptableObject {

    public string unNombre = "New Ability";
    public Sprite unaImagen;
    public AudioClip unSonido;
    public float coolDownBase = 1f;

    public abstract void Inicializa(GameObject obj);
    public abstract void Ejecuta();
}

