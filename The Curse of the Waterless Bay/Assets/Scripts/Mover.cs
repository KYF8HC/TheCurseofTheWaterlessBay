using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider2D;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;
    protected virtual void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reset moveDelta
        moveDelta = input;
        //Swap sprite direction wheter you're going right or left
        if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDelta.x > 0)
            transform.localScale = Vector3.one;

        //Add push vector
        moveDelta += pushDirection; 
        //Reduce the force every frame
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Testing colliding on y axis
        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            //Move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        //Testing colliding on y axis
        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            //Move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
