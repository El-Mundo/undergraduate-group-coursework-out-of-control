using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (transform.localScale.y <= 0.02f)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }
}
