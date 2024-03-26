using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    [SerializeField] private float kunaiSpeed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = transform.right * kunaiSpeed * Time.timeScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        kunaiSpeed = 0;
        PlayerController.instance.DeactivateBreakTime();

        if(collision.gameObject.CompareTag("Barrier"))
        {
            PlayerController.instance.RemoveSpawnedKunai();
            if (PlayerController.instance.isJumping) return;
            PlayerController.instance.RecoverKunai();
        }
    }
}
