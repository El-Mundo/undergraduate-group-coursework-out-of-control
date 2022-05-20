using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySceneBehaviour : MonoBehaviour
{
    public SceneController scene;

    void Start()
    {
        scene.Level();
    }

}
