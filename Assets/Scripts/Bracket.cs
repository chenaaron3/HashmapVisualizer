using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracket : Operator
{
    public override void OnTick()
    {
        if (node != null)
        {
            // stops the node
            node.SetDirection(Vector2.zero);
            // pushes the bracket over
            transform.position += transform.right;
            // complete process
            Generator.instance.processing = false;
        }
    }
}
