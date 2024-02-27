using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamaController : MonoBehaviour
{
    [SerializeField] private float kamaSpeed = 20f;

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
        if (collision.gameObject.CompareTag("Wood") || collision.gameObject.CompareTag("Wood Ground"))
        {
            PlayerController.instance.MovePlayerToKama();
        }
        else
        {
            PlayerController.instance.DeactivateBreakTime();
            PlayerController.instance.RemoveSpawnedKama();
        }
    }
}
