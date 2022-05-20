using UnityEngine;

/// <summary>
/// A universal aesthetical script written by Shuanyuan Cao.
/// </summary>
public class LoopableSprite : MonoBehaviour
{
    [SerializeField]
    private LoopableSprite counterpart;
    [SerializeField]
    private Transform loopPosition;
    public Transform loopPoint;
    public float offset;

    public Vector3 velocity;

    void Start()
    {
        offset = loopPoint.position.x - transform.position.x;
    }

    void Update()
    {
        if(loopPoint.position.x < loopPosition.position.x)
        {
            //Loop happens
            transform.position = counterpart.loopPoint.position + new Vector3(offset, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        transform.position += velocity;
    }

}
