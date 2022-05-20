using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearAreaBeahviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public bool touched = false;
    public GameObject indicator;

    private readonly static Vector3 INDICATOR_POS = new Vector3(0, 2.5F, 0);

    private void Awake()
    {
        indicator.transform.position = transform.position + INDICATOR_POS;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            touched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            touched = false;
        }
    }

    private void Update()
    {
        indicator.SetActive(touched);
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && PlayerBehaviour.instance.controls.KeyUsable("Up") && touched && GameManager.instance.state < 4)
        {
            GameManager.instance.LevelClear();
            animator.Play("Clear", 1);
        }
    }

}
