using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Operator
{
    public override void OnTick()
    {
        if (node != null)
        {
            // consumes the hashtag
            node.HideHashtag();
            // teleports the node to the right hash
            int bucketIndex = node.GetHash();
            Bucket targetBucket = BuildManager.instance.buckets[bucketIndex];
            node.transform.parent = targetBucket.nodesBucket.transform;
            node.transform.position = targetBucket.transform.position;
            targetBucket.node = node;
            targetBucket.OnTick();       
        }
    }
}
