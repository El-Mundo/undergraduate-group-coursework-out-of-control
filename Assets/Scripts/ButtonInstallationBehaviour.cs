using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstallationBehaviour : MonoBehaviour
{
    private readonly static float DEFAULT_COL_OFFSET_Y = -0.4580388F,
        PRESSED_COL_OFFSET_Y = -0.7F, COL_OFFSET_X = -0.006636798F;

    public int index;
    private bool isOn = false;
    SpriteRenderer sprite;
    public Sprite on, off;
    public AudioClip clip;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Moving"))
        {
            isOn = true;
            GameManager.instance.SetSceneBool(index, isOn);
            sprite.sprite = on;
            GameManager.instance.PlaySFX(clip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Moving"))
        {
            isOn = false;
            GameManager.instance.SetSceneBool(index, isOn);
            sprite.sprite = off;
        }
    }

}
