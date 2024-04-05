using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    GameObject kunai;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            kunai = GameObject.Find("Kunai(Clone)");
            this.transform.position = kunai.transform.position;
        }
        catch
        {;}
        
    }
}
