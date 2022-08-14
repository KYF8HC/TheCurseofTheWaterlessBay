using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    //Damage
    public int damage;
    public float knockBack;

    protected override void OnCollide(Collider2D other)
    {
        if (other.tag == "Fighter" && other.name == "Player")
        {
            //Create a new damage object, before sending it to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                knockBack = this.knockBack,
                origin = transform.position
            };
            other.SendMessage("ReceiveDamage", dmg);
        }
    }
}