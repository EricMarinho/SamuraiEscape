using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamaController : MonoBehaviour
{
    [SerializeField] private float kamaSpeed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = transform.right * kamaSpeed * Time.timeScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        kamaSpeed = 0;
        PlayerController.instance.DeactivateBreakTime();
    }
}
