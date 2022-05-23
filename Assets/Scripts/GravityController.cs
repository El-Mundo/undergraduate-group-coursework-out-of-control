using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (transform.localScale.y <= 0.02f)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }
}
