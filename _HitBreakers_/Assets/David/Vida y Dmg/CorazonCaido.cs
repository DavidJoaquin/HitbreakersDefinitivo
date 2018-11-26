using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonCaido : MonoBehaviour
{

    public GameObject miDueño;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == miDueño.gameObject)
        {
            miDueño.GetComponent<HealthManager>().RecuperarCorazon();
            Destroy(this.gameObject);
        }else if(other.gameObject.tag == "RecibeDmg")
        {
            Destroy(this.gameObject);
        }

        
    }

}