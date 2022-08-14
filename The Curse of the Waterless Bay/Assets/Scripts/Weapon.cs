using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //DamageStruct
    public int damagePoint = 1;
    public float knockBack = .5f;

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private float cooldown = .5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                knockBack = this.knockBack,
                origin = transform.position
            };
            other.SendMessage("ReceiveDamage", dmg);
        }
    }
    private void Swing()
    {

    }
}
