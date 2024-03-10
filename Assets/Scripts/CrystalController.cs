using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnCrystalRestored += OnCrystalRestored;
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
