using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    public int index;
    [SerializeField]
    private bool isOn = false;
    SpriteRenderer sprite;
    public Sprite on, off;
    public AudioClip clip;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = isOn ? on : off;
        GameManager.instance.SetSceneBool(index, isOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOn = !isOn;
            GameManager.instance.SetSceneBool(index, isOn);
            sprite.sprite = isOn ? on : off;
            GameManager.instance.PlaySFX(clip);
        }
    }

}
