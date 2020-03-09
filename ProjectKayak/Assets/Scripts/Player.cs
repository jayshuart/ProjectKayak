using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float PADDLE_POWER = 50;
    [SerializeField] private float PADDLE_TURN_POWER = 5;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        animator =  this.gameObject.GetComponent<Animator>();

        //animator.Play("Paddle_idle");
        animator.Play("float");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            PaddleForward(1);

            animator.Play("row_right");
            animator.Play("row_left");
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            PaddleForward(-1);

            animator.Play("row_right");
            animator.Play("row_left");
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PaddleTurn(-1);
            animator.Play("row_left");
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            PaddleTurn(1);
            animator.Play("row_right");
        }
    }

    private void PaddleForward(int pDir)
    {
        Vector3 paddleForce = this.transform.forward * PADDLE_POWER * pDir;
        rb.AddForce(paddleForce, ForceMode.Impulse);
        //rb.velocity += paddleForce;
    }

    private void PaddleTurn(int pDir)
    {
        Vector3 paddleForce = new Vector3(0, PADDLE_TURN_POWER * pDir, 0);
        rb.AddTorque(paddleForce);
    }
}
