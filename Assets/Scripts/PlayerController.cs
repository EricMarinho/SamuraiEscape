using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [Range(0f, 10f)]
    [SerializeField] private float teleportBreakTime = 0.2f;

    [SerializeField] private Transform kunaiOrigin;
    [SerializeField] private GameObject kunaiPrefab;
    [SerializeField] private Transform kamaOrigin;
    [SerializeField] private GameObject kamaPrefab;

    public bool isJumping = false;
    public bool hasKama = true;
    public bool hasKunai = true;

    private Rigidbody2D rb;

    private GameObject spawnedKunai = null;
    private GameObject spawnedKama = null;

    //Instance
    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Teleport();
            ShootKunai();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ThrowKama();
        }

        if (isJumping) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void ActivateBreakTime()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    public void DeactivateBreakTime()
    {
        rb.gravityScale = 1;
    }

    public void ActivateBreakTimeWithTime(float breakTime)
    {
        StopAllCoroutines();
        StartCoroutine(BreakTime(breakTime));
    }

    private IEnumerator BreakTime(float breakTime)
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(breakTime);
        rb.gravityScale = 1;
    }

    private void Teleport()
    {
        if(spawnedKunai == null) return;
 
        rb.transform.position = spawnedKunai.transform.position;
        rb.velocity = Vector2.zero;

        RemoveSpawnedKunai();
        ActivateBreakTimeWithTime(teleportBreakTime);
    }

    private void ShootKunai()
    {
        if(!hasKunai) return;

        ActivateBreakTime();
        hasKunai = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - kunaiOrigin.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spawnedKunai = Instantiate(kunaiPrefab, kunaiOrigin.position, Quaternion.Euler(0, 0, angle));

        kunaiOrigin.gameObject.SetActive(false);
    }

    private void ThrowKama()
    {
        if (!hasKama) return;

        ActivateBreakTime();
        hasKama = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - kunaiOrigin.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spawnedKama = Instantiate(kamaPrefab, kamaOrigin.position, Quaternion.Euler(0, 0, angle));

        kamaOrigin.gameObject.SetActive(false);
    }

    private void MovePlayerToKama()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        //lerp the player position to the kama position and then reset the gravity scale
    }

    public void RemoveSpawnedKunai()
    {
        Destroy(spawnedKunai);
        spawnedKunai = null;
    }

    public void RecoverKunai()
    {
        hasKunai = true;
        kunaiOrigin.gameObject.SetActive(true);
    }

    public void RemoveSpawnedKama()
    {
        Destroy(spawnedKama);
        spawnedKama = null;
    }

    public void RecoverKama()
    {
        hasKama = true;
        kamaOrigin.gameObject.SetActive(true);
    }
}
