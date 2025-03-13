using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemToDrop;

    public void dropItem(Vector3 position)
    {
        Instantiate(itemToDrop, position, Quaternion.identity);
    }
}
