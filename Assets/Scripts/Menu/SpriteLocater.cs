using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteLocater : MonoBehaviour
{

    //Align the sprite to the right border of screen
    void Update()
    {
        float x = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        transform.position = new Vector3(x, transform.position.y);
    }

}
