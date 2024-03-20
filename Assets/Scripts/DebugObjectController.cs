using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObjectController : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] GameObject poi;

    public float velocity = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            this.transform.position = new Vector3(this.transform.position.x - velocity, this.transform.position.y, 0);
        }
        else if (Input.GetKey("d"))
        {
            this.transform.position = new Vector3(this.transform.position.x + velocity, this.transform.position.y, 0);
        }

        if (Input.GetKey("w"))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + velocity, 0);
        }
        else if (Input.GetKey("s"))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - velocity, 0);
        }

        if (Input.GetKey("r"))
        {
            this.transform.position = new Vector3(-9.25f, -2.25f, 0);

            //cam.transform.position = new Vector3(0, this.transform.position.y + cam.orthographicSize * 0.5f, -10);
            cam.transform.position = poi.transform.position;
       

        }
    }
}
