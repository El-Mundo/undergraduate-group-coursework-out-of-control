using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;

    [Header("Player Settings")]
    [SerializeField]
    private float jumpVelocity = 8.0f;
    [SerializeField]
    private float walkVelocity = 4.0f, shiftVelocity = 2.0f;
    [SerializeField]
    private float runningScale = 2.0f, runningJumpScale = 1.5f;
    private static readonly float AXIS_DEAD_ZONE = 0.05F, VEL_DEAD_ZONE = 0.1F;
    private static readonly float JUMPING_START_DEADZONE = 0.5F;

    [Header("Setups")]
    [SerializeField]
    private Transform spriteTransform;
    [SerializeField]
    private Animator spriteAnimator;
    [SerializeField]
    private PlayerAnimator animScript;

    [Header("In-Game States")]
    public bool landed = false;
    public bool running = false;
    public bool rightBlocked = false, leftBlocked = false;
    public bool inConversation = false;
    public bool active = true;
    public float xMov = 0;
    private Rigidbody2D rbody;
    [Tooltip("The speed of the platform that player steps on.")]
    public Vector2 baseVelocity = Vector2.zero;

    public ControlDisabler controls;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        controls = new ControlDisabler();
    }

    private void Update()
    {
        int st = GameManager.instance.state;
        if (st == 2 || st == 4 || st == 5) return;

        if (landed && active && !inConversation)
        {
            running = false;

            if(Input.GetButton("Run") && controls.KeyUsable("Run"))
            {
                running = true;
            }
            if (Input.GetButton("Jump") && controls.KeyUsable("Jump") && rbody.velocity.y < JUMPING_START_DEADZONE)
            {
                Jump();
            }
        }

        float h = (active && !inConversation) ? Input.GetAxisRaw("Horizontal") : 0;
        if(h < -AXIS_DEAD_ZONE && controls.KeyUsable("Left"))
        {
            //if(!leftBlocked) rbody.velocity = new Vector2(-walkVelocity, rbody.velocity.y);
            if (!leftBlocked) xMov = -walkVelocity * (running ? runningScale : 1);
            if (spriteTransform.localScale.x > 0) spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if(h > AXIS_DEAD_ZONE && controls.KeyUsable("Right"))
        {
            //if(!rightBlocked) rbody.velocity = new Vector2(walkVelocity, rbody.velocity.y);
            if (!rightBlocked) xMov = walkVelocity * (running ? runningScale : 1);
            if (spriteTransform.localScale.x < 0) spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            //float vx = rbody.velocity.x;
            //rbody.velocity = new Vector2(Mathf.MoveTowards(vx, 0, shiftVelocity), rbody.velocity.y);
            xMov = Mathf.MoveTowards(xMov, 0, shiftVelocity);
        }
        //bool walking = Mathf.Abs(rbody.velocity.x) > VEL_DEAD_ZONE;
        bool walking = Mathf.Abs(xMov) > VEL_DEAD_ZONE;

        spriteAnimator.SetBool("running", running);

        //Add the speed of the platform being stepped on.
        rbody.velocity = new Vector2(baseVelocity.x + xMov, rbody.velocity.y);
        if (rbody.velocity.x < -VEL_DEAD_ZONE && leftBlocked) ClearXVelocity();
        else if (rbody.velocity.x > VEL_DEAD_ZONE && rightBlocked) ClearXVelocity();

        spriteAnimator.SetBool("landed", landed);
        spriteAnimator.SetBool("walking", walking);
    }

    private void ClearXVelocity()
    {
        rbody.velocity = new Vector2(0, rbody.velocity.y);
    }

    private void Jump()
    {
        rbody.velocity = new Vector2(rbody.velocity.x, jumpVelocity * (running ? runningJumpScale : 1));
        animScript.Jump();
    }

    public void Defeat()
    {
        if (active) {
            spriteAnimator.SetBool("defeated", true);
            animScript.Defeat();
            active = false;
        }
    }

    public void StepOnGrass(bool isOnGrass)
    {
        animScript.SetOnGrass(isOnGrass);
    }

    public void Footstep()
    {
        animScript.Footstep();
    }

    public void ShutDownFootstep()
    {
        animScript.Stop();
    }

}
