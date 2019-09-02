using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hashtag : Operator
{
    public override void OnTick()
    {
        if (node != null)
        {
            // sets the hash to the string's hashcode
            node.SetHash(node.GetValue().ToString().GetHashCode());
        }
    }
}
