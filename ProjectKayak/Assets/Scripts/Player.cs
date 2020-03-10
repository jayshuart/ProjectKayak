using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float PADDLE_POWER = 50;
    [SerializeField] private float PADDLE_TURN_POWER = 5;

    private Animator animator;
    private Gestures gestureHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        animator =  this.gameObject.GetComponent<Animator>();
        gestureHandler = this.gameObject.GetComponent<Gestures>();

        //animator.Play("Paddle_idle");
        animator.Play("float");

        //setup gesture handler functions
        gestureHandler.event_SwipeDown.AddListener(PaddleForward);
        gestureHandler.event_SwipeUp.AddListener(PaddleBackward);
        gestureHandler.event_SwipeLeft.AddListener(PaddleRight);
        gestureHandler.event_SwipeRight.AddListener(PaddleLeft);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            PaddleForward();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            PaddleBackward();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PaddleLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
           PaddleRight();
        }
    }

    private void PaddleForward() 
    {
        StartCoroutine(DelayedMoveForward(.3f));
        animator.Play("row_right");
        animator.Play("row_left");
    }

    private void PaddleBackward()
    {
        MoveForward(-1);
        animator.Play("row_right");
        animator.Play("row_left");
    }

    private void PaddleLeft()
    {
        Turn(-1);
        animator.Play("row_left");
    }

    private void PaddleRight()
    {
        Turn(1);
        animator.Play("row_right");
    }

    IEnumerator DelayedMoveForward(float time)
    {
        yield return new WaitForSeconds(time);
        MoveForward(1);
    }

    private void MoveForward(int pDir)
    {
        Vector3 paddleForce = this.transform.forward * PADDLE_POWER * pDir;
        rb.AddForce(paddleForce, ForceMode.Impulse);
        //rb.velocity += paddleForce;
    }

    private void Turn(int pDir)
    {
        Vector3 paddleForce = new Vector3(0, PADDLE_TURN_POWER * pDir, 0);
        rb.AddTorque(paddleForce);
    }
}
