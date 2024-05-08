using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    [SerializeField] private float kunaiSpeed = 5f;
    [SerializeField] private float kunaiRadius = 0.2f;
    [SerializeField] private float kunaiDetectingDistance = 0.05f;

    private Rigidbody2D rb;
    private int layer_mask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = transform.right * kunaiSpeed * Time.timeScale;

        RaycastHit2D hit = Physics2D.CircleCast(rb.transform.position, kunaiRadius, rb.transform.position, kunaiDetectingDistance);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
                return;

            Debug.Log(hit.collider.name);
            kunaiSpeed = 0;
            PlayerController.instance.DeactivateBreakTime();

            if (hit.collider.gameObject.CompareTag("Barrier"))
            {
                PlayerController.instance.RemoveSpawnedKunai();
                if (PlayerController.instance.isJumping) return;
                PlayerController.instance.RecoverKunai();
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    kunaiSpeed = 0;
    //    PlayerController.instance.DeactivateBreakTime();

    //    if(collision.gameObject.CompareTag("Barrier"))
    //    {
    //        PlayerController.instance.RemoveSpawnedKunai();
    //        if (PlayerController.instance.isJumping) return;
    //        PlayerController.instance.RecoverKunai();
    //    }
    //}
}
