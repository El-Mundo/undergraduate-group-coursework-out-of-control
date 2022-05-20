using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [Header("Setuup")]
    public GameObject player;
    private Rigidbody2D pBody;
    [SerializeField]
    private Transform leftBorder, rightBorder;
    [SerializeField]
    private float moveTolerance;
    [SerializeField]
    private MiddleParallax parallax;

    //public float speed;
    [Header("Settings")]
    public float minPosx = 0;
    public float maxPosx = 0;
    public float easing = 0.01f;
    public float range = 1.0f;
    public float offset = 5;
    public Transform playerSprite;
    float realOffset = 0.0f;
    public bool trackY;
    public float yOffset = -2.5f;
    public float minPosY = 0, maxPosY = 2;
    public bool follow = true;

    void Start()
    {
        //trackY = false;
        pBody = player.GetComponent<Rigidbody2D>();
        minPosx = leftBorder.position.x + moveTolerance;
        maxPosx = rightBorder.position.x - moveTolerance;
    }

    void FixedUpdate()
    {
        if(follow) {
            FixCameraPos();
        }
    }

    void Update()
    {
        if(trackY && follow) {
            FixYAxis();
        }
        parallax.Move((transform.position.x - minPosx) / (maxPosx - minPosx));
    }

    void FixYAxis()
    {
        float realPosY = Mathf.Clamp(player.transform.position.y - yOffset, minPosY, maxPosY);
        transform.position = new Vector3(transform.position.x, realPosY, transform.position.z);
    }

    void FixCameraPos()
    {
        if (pBody.velocity.x > 0.02F) 
        {
            realOffset = offset;
        }
        else if(pBody.velocity.x < -0.02F)
        {
            realOffset = -offset;
        }
        else
        {
            if(playerSprite.localScale.x < 0) {
                realOffset = -offset;
            }else {
                realOffset = offset;
            }
        }

        float pPosX = player.transform.position.x + realOffset;
        pPosX = Mathf.Clamp(pPosX, minPosx, maxPosx);

        float cPosX = transform.position.x;
        if (pPosX - cPosX > range) {
            transform.position = new Vector3(cPosX + (pPosX - cPosX) * easing, transform.position.y, transform.position.z);
        }else if (pPosX - cPosX < -range) {
            transform.position = new Vector3(cPosX + (pPosX - cPosX) * easing, transform.position.y, transform.position.z);
        }
        float realPosX = Mathf.Clamp(transform.position.x, minPosx, maxPosx);

        transform.position = new Vector3(realPosX, transform.position.y, transform.position.z);
    }

    public void UpdateCamera()
    {
        float realPosX = Mathf.Clamp(player.transform.position.x + realOffset, minPosx, maxPosx);
        transform.position = new Vector3(realPosX, player.transform.position.y - yOffset, transform.position.z);
    }

    public void MoveCamera(Transform tr)
    {
        transform.position = tr.position;
    }

}
