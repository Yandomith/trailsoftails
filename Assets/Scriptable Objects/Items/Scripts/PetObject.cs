using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pet Object", menuName = "Inventory System/Items/Pet")]
public class PetObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Pet;
    }
}
