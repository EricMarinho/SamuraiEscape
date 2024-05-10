//sing System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class FootHandler : MonoBehaviour
{

    private Animator playerAnimation;

    private void Start()
    {
        playerAnimation = GetComponentInParent<Animator>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood Ground"))
        {
            PlayerController.instance.isJumping = false;
            PlayerController.instance.RecoverKunai();
            PlayerController.instance.RemoveSpawnedKunai();
            PlayerController.instance.DisableDash();
            GameEvents.Instance.OnCrystalRestored?.Invoke();

            if (PlayerController.instance.isJumping) return;
            PlayerController.instance.RecoverKunai();

            //PlayerController.instance.RemoveSpawnedKama();

            //if(!PlayerController.instance.isMovingWithKama && !PlayerController.instance.isOnKama)
            //{
            //    PlayerController.instance.RecoverKama();
            //}

            playerAnimation.SetBool("Jumping", PlayerController.instance.isJumping);
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