using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int maxHealth;
    private int currentHealth;
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;

    public CorazonCaido corazonCaid;
    public Transform spawnCorazon;


    // Use this for initialization
    void Start () {

        currentHealth = maxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
		
	}


    public void RecibirDmg (int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        CorazonCaido newCorazon = Instantiate(corazonCaid, spawnCorazon.position, spawnCorazon.rotation) as CorazonCaido;
        newCorazon.miDueño = this.gameObject;

        switch (currentHealth)
        {
            case 3:
                break;
            case 2:
                corazon3.SetActive(false);
                break;
            case 1:
                corazon2.SetActive(false);
                break;
            case 0:
                corazon1.SetActive(false);
                break;

        }

    }

    public void RecuperarCorazon()
    {
        currentHealth += 1;

        switch (currentHealth)
        {
            case 3:
                corazon3.SetActive(true);
                break;
            case 2:
                corazon2.SetActive(true);
                break;
            case 1:
                corazon1.SetActive(true);
                break;

        }
    }

}
