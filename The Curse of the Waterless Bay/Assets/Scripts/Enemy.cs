using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = .2f;
    public float chaseLength = .5f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform PlayerTransform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D filter2D;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        PlayerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //Is the player in range?
        if (Vector3.Distance(PlayerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(PlayerTransform.position, startingPosition) < triggerLength)
                chasing = true;
            
            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((PlayerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider2D.OverlapCollider(filter2D, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
                collidingWithPlayer = true;
            hits[i] = null;
        }
        UpdateMotor(Vector3.zero);
    }
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
