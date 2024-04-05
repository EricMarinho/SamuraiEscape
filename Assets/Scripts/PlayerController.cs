using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float dashTime = 0.35f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private BoxCollider2D barrierCollider;

    [Header("Kunai")]
    [SerializeField] private float teleportBreakTime = 1f;
    [SerializeField] private Transform kunaiOrigin;
    [SerializeField] private GameObject kunaiPrefab;

    [Header("Crystal")]
    [SerializeField] private float crystalBreakTime = 1f;

    public bool isJumping = false;

    public float jumpTimeStart = 0;

    public bool hasKunai = true;
    public bool hasDash = false;
    private bool isDashing = false;

    private Vector2 dashDirection;

    private float horizontal => Input.GetAxis("Horizontal");
    private float vertical => Input.GetAxis("Vertical");

    private Rigidbody2D rb;

    private GameObject spawnedKunai = null;

    private float lerpTimer = 0f;
    private float dashTimer = 0f;
    private float currentPlayerSpeed;

    //[SerializeField] private float kamaBreakTime = 1f;
    //[SerializeField] private float kamaTeleportSpeed = 0.1f;
    //[SerializeField] private Transform kamaOrigin;
    //[SerializeField] private GameObject kamaPrefab;
    //public bool hasKama = true;
    //public bool isMovingWithKama = false;
    //public bool isOnKama = false;
    //private GameObject spawnedKama = null;

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

        currentPlayerSpeed = playerSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameEvents.Instance.OnCrystalCollected += OnCrystalCollected;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnCrystalCollected -= OnCrystalCollected;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping) return;
            Jump();
        }

        //if (isMovingWithKama)
        //{
        //    LerpPlayerPosition();
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hasDash)
            {
                TryDash();
                return;
            }
            Teleport();
            ShootKunai();  
        }

        if (isDashing)
        {
            Dash();
            return;
        }

        //if (isOnKama) return;

        if (horizontal < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        MovePlayer();

        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    ThrowKama();
        //}
    }

    private void MovePlayer()
    {
        if (isJumping) return;
        rb.velocity = new Vector2(horizontal * currentPlayerSpeed, rb.velocity.y);
    }

    private void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Dash()
    {
        rb.velocity = Vector2.zero;
        rb.transform.position += new Vector3(dashDirection.x, dashDirection.y, 0f) * dashSpeed * Time.deltaTime;
        dashTimer += Time.deltaTime;
        if (dashTimer > dashTime)
        {
            StopDash();
            rb.gravityScale = 1f;
            //isOnKama = false;
            DeactivateBreakTime();
            currentPlayerSpeed = playerSpeed;
        };
    }

    public void Jump()
    {
        isJumping = true;

        jumpTimeStart = Time.time;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void EnableDashCollider()
    {
        barrierCollider.enabled = true;
    }

    private void DisableDashCollider()
    {
        barrierCollider.enabled = false;
    }

    private void TryDash()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        isDashing = true;
        EnableDashCollider();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dashDirection = (mousePosition - rb.transform.position).normalized;

        float angle = Mathf.Atan2(dashDirection.y, dashDirection.x);
        //int octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
        //float quantizedAngle = octant * (2 * Mathf.PI) / 8;
        dashDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Debug.Log(dashDirection);

        DisableDash();

        //if(spawnedKama != null)
        //{
        //    RemoveSpawnedKama();
        //}
    }

    public void ActivateBreakTime(bool enableDash = false)
    {
        StopAllCoroutines();
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        currentPlayerSpeed = 0f;

        if (!enableDash) return;

        EnableDash();
    }

    public void DeactivateBreakTime()
    {
        StopAllCoroutines();
        DisableDash();
        rb.gravityScale = 1;
        currentPlayerSpeed = playerSpeed;

        //isOnKama = false;
        //if (spawnedKama != null)
        //{
        //    RemoveSpawnedKama();
        //}
    }

    public void ActivateBreakTimeWithTime(float breakTime, bool enableDash = false)
    {
        StopAllCoroutines();
        StartCoroutine(BreakTime(breakTime, enableDash));
    }

    private IEnumerator BreakTime(float breakTime, bool enableDash = false)
    {
        if(enableDash) EnableDash();
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        currentPlayerSpeed = 0f;
        yield return new WaitForSeconds(breakTime);
        rb.gravityScale = 1;
        currentPlayerSpeed = playerSpeed;

        DisableDash();

        //isOnKama = false;        
        //RemoveSpawnedKama();

    }

    private void Teleport()
    {
        if(spawnedKunai == null) return;
 
        rb.transform.position = spawnedKunai.transform.position;
        rb.velocity = Vector2.zero;

        RemoveSpawnedKunai();
        ActivateBreakTimeWithTime(teleportBreakTime, true);

        //RemoveSpawnedKama();
    }

    public void EnableDash()
    {
        dashTimer = 0f;
        hasDash = true;
        playerSpriteRenderer.color = Color.blue;
    }

    public void DisableDash()
    {
        hasDash = false;
        playerSpriteRenderer.color = Color.white;
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

    private void OnCrystalCollected()
    {
        //StopDash();
        RemoveSpawnedKunai();
        RecoverKunai();
        //ActivateBreakTimeWithTime(crystalBreakTime);
    }

    private void StopDash()
    {
        isDashing = false;
        dashTimer = 0f;
        DisableDashCollider();
    }

    //private void ThrowKama()
    //{
    //    if (!hasKama) return;

    //    ActivateBreakTime();
    //    hasKama = false;

    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 direction = (mousePos - kunaiOrigin.position).normalized;

    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    spawnedKama = Instantiate(kamaPrefab, kamaOrigin.position, Quaternion.Euler(0, 0, angle));

    //    kamaOrigin.gameObject.SetActive(false);
    //}

    //public void MovePlayerToKama()
    //{
    //rb.gravityScale = 0;
    //rb.velocity = Vector2.zero;
    //lerpTimer = 0f;
    //isMovingWithKama = true;
    //}

    //public void RemoveSpawnedKama()
    //{
    //if (spawnedKama == null) return;
    //if (isMovingWithKama) return;
    //Destroy(spawnedKama);
    //spawnedKama = null;
    //}

    //public void RecoverKama()
    //{
    //hasKama = true;
    //kamaOrigin.gameObject.SetActive(true);
    //}

    //void LerpPlayerPosition()
    //{
    //    lerpTimer += Time.deltaTime;

    //    if (lerpTimer < kamaTeleportSpeed)
    //    {
    //        float t = lerpTimer / kamaTeleportSpeed;
    //        rb.transform.position = Vector3.Lerp(kamaOrigin.position, spawnedKama.transform.position, t);
    //    }
    //    else
    //    {
    //        rb.transform.position = spawnedKama.transform.position;
    //        RemoveSpawnedKama();
    //        RecoverKunai();
    //        RemoveSpawnedKunai();
    //        ActivateBreakTimeWithTime(kamaBreakTime);
    //        isJumping = true;
    //        isMovingWithKama = false;
    //        isOnKama = true;
    //    }
    //}
}