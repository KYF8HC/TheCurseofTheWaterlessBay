using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite EmptyChestSprite;
    public int goldAmount = 10;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent <SpriteRenderer>().sprite = EmptyChestSprite;
            Debug.Log("Grant " + goldAmount + " Pesos!");
        }
    }
}
