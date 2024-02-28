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
    [SerializeField] private float kamaTeleportSpeed = 0.1f;
    [SerializeField] private float dashTime = 0.35f;
    [SerializeField] private float dashSpeed = 10f;

    [SerializeField] private Transform kunaiOrigin;
    [SerializeField] private GameObject kunaiPrefab;
    [SerializeField] private Transform kamaOrigin;
    [SerializeField] private GameObject kamaPrefab;

    public bool isJumping = false;
    public bool hasKama = true;
    public bool hasKunai = true;
    public bool hasDash = false;
    public bool isMovingWithKama = false;
    private bool isDashing = false;
    public bool isOnKama = false;

    private Vector2 dashDirection;

    private float horizontal => Input.GetAxis("Horizontal");
    private float vertical => Input.GetAxis("Vertical");

    private Rigidbody2D rb;

    private GameObject spawnedKunai = null;
    private GameObject spawnedKama = null;

    private float lerpTimer = 0f;
    private float dashTimer = 0f;

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
        if (isDashing)
        {
            rb.velocity = Vector2.zero;
            rb.transform.position += new Vector3(dashDirection.x, dashDirection.y, 0f) * dashSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;
            if (dashTimer > dashTime)
            {
                Debug.Log("Finished Dash");
                isDashing = false;
                dashTimer = 0f;
                rb.gravityScale = 1f;
                isOnKama = false;
            };
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isJumping && hasDash)
            {
                Dash();
                return;
            }

            if (isJumping) return;

            Jump();
        }

        if (isMovingWithKama)
        {
            LerpPlayerPosition();
            return;
        }

        if (isOnKama) return;

        if (horizontal < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Teleport();
            ShootKunai();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ThrowKama();
        }
    }

    void LerpPlayerPosition()
    {
        lerpTimer += Time.deltaTime;

        if (lerpTimer < kamaTeleportSpeed)
        {
            float t = lerpTimer / kamaTeleportSpeed;
            rb.transform.position = Vector3.Lerp(kamaOrigin.position, spawnedKama.transform.position, t);
        }
        else
        {
            rb.transform.position = spawnedKama.transform.position;
            RemoveSpawnedKama();
            hasDash = true;
            isJumping = true;
            isMovingWithKama = false;
            isOnKama = true;
        }
    }

    public void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Dash()
    {
        Debug.Log("Dashing");
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        dashDirection = new Vector2(horizontal, vertical).normalized;
        isDashing = true;
        hasDash = false;
        if(spawnedKama != null)
        {
            RemoveSpawnedKama();
        }
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
        hasDash = true;
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

    public void MovePlayerToKama()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        lerpTimer = 0f;
        isMovingWithKama = true;
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
        if (isMovingWithKama) return;
        Destroy(spawnedKama);
        spawnedKama = null;
    }

    public void RecoverKama()
    {
        hasKama = true;
        kamaOrigin.gameObject.SetActive(true);
    }
}
