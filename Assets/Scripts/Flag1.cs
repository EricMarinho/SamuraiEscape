using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flag1 : MonoBehaviour
{
    [SerializeField] Flag flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(flag.triggerFlagTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLISAO FLAG");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER FLAG 2");

        Debug.Log("Time: " + (Time.time - flag.triggerFlagTime));
    }
}
