using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerController.instance.isJumping = false;
            PlayerController.instance.RecoverKunai();
            PlayerController.instance.RemoveSpawnedKunai();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerController.instance.isJumping = true;
        }
    }
}