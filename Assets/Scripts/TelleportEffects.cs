using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelleportEffects : MonoBehaviour
{

    public float throwForce = 5f; // Initial upward force
    public float destroyTime = 2f; // Time before the object is destroyed

    [SerializeField] GameObject trunk;
    [SerializeField] GameObject fumacaBrilho;

    private Rigidbody2D rb;

    GameObject spawnedTrunk;
    GameObject teleportEffect;

    public void Spawn()
    {

        if (rb == null)
        {
            // Add Rigidbody component to the GameObject if not already present
            spawnedTrunk = Instantiate(trunk, transform.position, Quaternion.identity);
            teleportEffect = Instantiate(fumacaBrilho, transform.position, Quaternion.identity);

            rb = spawnedTrunk.GetComponent<Rigidbody2D>();

            // Apply upward force
            rb.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);

            // Start the destroy timer
            Destroy(spawnedTrunk, destroyTime);
            Destroy(teleportEffect, destroyTime);
        }

    }

    private void Update()
    {
        if (Input.GetKey("p")) Spawn();

        if (spawnedTrunk != null) spawnedTrunk.transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);

    }
}