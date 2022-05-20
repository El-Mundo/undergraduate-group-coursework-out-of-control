using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleParallax : MonoBehaviour
{
    [SerializeField]
    Transform left, right;
    [SerializeField]
    Transform sceneLeft, sceneRight;
    float minX, maxX;

    private void Start()
    {
        minX = left.position.x - sceneLeft.position.x;
        maxX = sceneRight.position.x - right.position.x;
    }

    /// <summary>
    /// Move the parrallax sprites.
    /// </summary>
    /// <param name="progress">(Current cam pos - cam start) / (cam end - cam start)</param>
    public void Move(float progress)
    {
        transform.position = new Vector3(progress * (maxX - minX) + minX, transform.position.y);
    }

}
