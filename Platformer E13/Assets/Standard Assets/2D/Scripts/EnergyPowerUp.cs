using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPowerUp : MonoBehaviour {

    [SerializeField]
    private Stat energy;


    private void Awake()
    {
        //energy.Initialize();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }
}
