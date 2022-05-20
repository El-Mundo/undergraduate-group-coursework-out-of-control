using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.instance.Defeat();
        }
    }
}
