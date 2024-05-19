using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string tutorialMessage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.Instance.OnTutorialTriggerEntered?.Invoke(tutorialMessage);
            Destroy(gameObject);
        }
    }
}
