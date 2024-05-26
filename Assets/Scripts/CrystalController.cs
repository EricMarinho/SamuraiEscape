using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{

    private GameObject crystalShine;

    private void Start()
    {
        GameEvents.Instance.OnCrystalRestored += OnCrystalRestored;

        //crystalShine = GameObject.Find("Shine");
        //crystalShine = Instantiate(crystalShine, transform.position, Quaternion.identity, crystalShine.transform);
        //crystalShine.transform.position = transform.position;
    }
    
    private void OnDestroy()
    {
        GameEvents.Instance.OnCrystalRestored -= OnCrystalRestored;
    }

    private void OnCrystalRestored()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEvents.Instance.OnCrystalCollected?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
