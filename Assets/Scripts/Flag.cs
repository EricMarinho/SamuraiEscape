using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public float triggerFlagTime = 0;

     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLISAO FLAG");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER FLAG 1");
        triggerFlagTime = Time.time; 
    }
}
