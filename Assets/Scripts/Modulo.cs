using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo : Operator
{
    public override void OnTick()
    {
        if (node != null)
        {
            // gets the number of buckets
            int numBuckets = BuildManager.instance.numBuckets;
            // finds the positive modulus value
            int val = node.GetHash() % numBuckets;
            val = val < 0 ? val + numBuckets : val;
            // compresses the hash value
            node.SetHash(val);
        }
    }
}
