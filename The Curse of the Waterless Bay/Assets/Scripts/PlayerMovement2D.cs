using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    private BoxCollider2D playerBoxCollider2D;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private void Start()
    {
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Reset moveDelta
        moveDelta = new Vector3(x, y, 0);

        //Swap sprite direction wheter you're going right or left
        if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDelta.x > 0)
            transform.localScale = Vector3.one;

        //Testing colliding on y axis
        hit = Physics2D.BoxCast(transform.position, playerBoxCollider2D.size, 0, new Vector2(0, moveDelta.y), 
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Background"));
        if (hit.collider == null)
        {
            //Move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        //Testing colliding on y axis
        hit = Physics2D.BoxCast(transform.position, playerBoxCollider2D.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Background"));
        if (hit.collider == null)
        {
            //Move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
