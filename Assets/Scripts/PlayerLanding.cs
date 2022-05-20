using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    public PlayerBehaviour player;
    [Tooltip("dir = 0 - landing; dir = 1 - right blocking; dir = 2 - left blocking")]
    public int dir = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer == 8 || (layer == 9 && dir != 0)) return;

        if (dir == 0) {
            player.landed = true;
            if (collision.CompareTag("Moving"))
            {
                player.baseVelocity = collision.attachedRigidbody.velocity;
                player.StepOnGrass(false);
            }
            else if (collision.CompareTag("Grass"))
            {
                player.baseVelocity = Vector2.zero;
                player.StepOnGrass(true);
            }
            else if (collision.CompareTag("Wood"))
            {
                player.baseVelocity = Vector2.zero;
                player.StepOnGrass(false);
            }
        }
        else if (dir == 1)
            player.rightBlocked = true;
        else if (dir == 2)
            player.leftBlocked = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer == 8 || (layer == 9 && dir != 0)) return;

        if (dir == 0) 
        {
            player.landed = false;
            player.baseVelocity = Vector2.zero;
        }
        else if (dir == 1)
            player.rightBlocked = false;
        else if (dir == 2)
            player.leftBlocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) return;

        if (dir == 0)
        {
            player.landed = true;
            player.ShutDownFootstep();
            if (collision.CompareTag("Grass"))
            {
                player.StepOnGrass(true);
            }
            else if (collision.CompareTag("Moving") || collision.CompareTag("Wood"))
            {
                player.StepOnGrass(false);
            }
            player.Footstep();
        }
    }

}
