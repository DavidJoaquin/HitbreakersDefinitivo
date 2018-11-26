using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    //FLOAT - La velocidad de la bala
    public float speed;
    public float tiempoVida;
    public int dmgBala;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Movemos la bala hacia delante con la velocidad speed multiplicada por Time.deltaTime
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        tiempoVida -= Time.deltaTime;
        if (tiempoVida <= 0)
        {
            Destroy(this.gameObject);
        }

		
	}


    private void OnCollisionEnter(Collision other){
        
        if(other.gameObject.tag == "RecibeDmg")
        {
            other.gameObject.GetComponent<HealthManager>().RecibirDmg(dmgBala);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Entorno")
        {
            Destroy(this.gameObject);
        }



    }

}
