using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour, IBreakable
{
    public void Break()
    {
        Destroy(gameObject);
    }
}