using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey("k")) this.transform.Translate(Vector2.left*Time.deltaTime);
        //if (Input.GetKey("l")) this.transform.Translate(Vector2.right*Time.deltaTime);

        this.transform.position = character.transform.position;

    }
}
