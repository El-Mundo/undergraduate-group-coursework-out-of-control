using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S5_Button : ButtonInstallationBehaviour
{
    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Moving") || collision.CompareTag("Wood"))
        {
            base.isOn = false;
            GameManager.instance.SetSceneBool(index, base.isOn);
            base.sprite.sprite = off;
        }
    }
}
