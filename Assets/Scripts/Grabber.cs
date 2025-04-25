using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    public InputActionReference grabLeft;
    public InputActionReference deviceVelocity;
    public GameObject grabbedObject;
 
    bool grabbed;
    Rigidbody rb;
    List<Vector3> trackingPos= new List<Vector3>();
    List<Vector3> trackingVelocity= new List<Vector3>();
    public float velocity = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        grabLeft.action.Enable();
        deviceVelocity.action.Enable();  
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Grabable")
            return;


        if (grabLeft.action.ReadValue<float>() == 1)
        {
            grabbed = true;
            
            grabbedObject=other.gameObject;
            rb = grabbedObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            Debug.Log(other.gameObject);
        }
      
    }
    // Update is called once per frame
    void Update()
    {
        if (grabbedObject == null)
            return;
        if (grabbed)
        {

            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.rotation = transform.rotation;
            if(trackingPos.Count > 15)
            {
                trackingPos.RemoveAt(0);
            }
            trackingPos.Add(transform.position);
            if (trackingVelocity.Count > 15)
            {
                trackingVelocity.RemoveAt(0);
            }
            trackingVelocity.Add(deviceVelocity.action.ReadValue<Vector3>());
        }
        if (grabLeft.action.ReadValue<float>() == 0)
        {
            grabbed = false;
            Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
            Vector3 velo = trackingVelocity [trackingPos.Count - 1] - trackingVelocity[0];
            rb.AddForce(direction * velocity);
            rb.useGravity = true;
            rb = null;
            grabbedObject =null;
            
            return;
        }
       
    }
}
