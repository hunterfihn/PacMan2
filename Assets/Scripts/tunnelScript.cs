using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelScript : MonoBehaviour
{
    public GameObject otherSpawn;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = this.otherSpawn.transform.position;
    }

    void Update()
    {
        
    }
}
