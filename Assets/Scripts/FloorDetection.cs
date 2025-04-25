using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    public Transform spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grabable")
        {
            other.gameObject.transform.position= spawnPoint.transform.position;


        }
    }
}
