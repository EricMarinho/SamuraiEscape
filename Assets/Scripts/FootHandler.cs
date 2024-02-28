using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood Ground"))
        {
            PlayerController.instance.isJumping = false;
            PlayerController.instance.hasDash = false;
            PlayerController.instance.RecoverKunai();
            PlayerController.instance.RemoveSpawnedKunai();
            PlayerController.instance.RemoveSpawnedKama();

            if(!PlayerController.instance.isMovingWithKama && !PlayerController.instance.isOnKama)
            {
                PlayerController.instance.RecoverKama();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood Ground"))
        {
            PlayerController.instance.isJumping = true;
        }
    }
}