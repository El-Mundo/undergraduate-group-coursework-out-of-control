using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInstallationBehaviour : MonoBehaviour
{
    public float openSpeed = 0.02f;
    public int index;
    [SerializeField]
    private Transform door, doorOpenedPos, doorClosedPos;
    public bool isOn;
    public AudioSource sound;

    void Update()
    {
        Vector3 doorPos = door.transform.localPosition;
        isOn = GameManager.instance.GetSceneBool(index);
        if (isOn)
        {
            if(doorPos.y < doorOpenedPos.localPosition.y)
            {
                door.localPosition += new Vector3(0, openSpeed * Time.deltaTime * 240, 0);
                if (!sound.isPlaying) sound.Play();
            }
        }
        else
        {
            if (doorPos.y > doorClosedPos.localPosition.y)
            {
                door.localPosition -= new Vector3(0, openSpeed * Time.deltaTime * 240, 0);
                if (!sound.isPlaying) sound.Play();
            }
        }
    }
}
