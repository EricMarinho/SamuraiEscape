using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnEndGame += EndGame;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnEndGame -= EndGame;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.Instance.OnEndGame?.Invoke();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
    }
}
