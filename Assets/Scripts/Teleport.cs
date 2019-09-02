﻿using System.Collections;
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
            node.Teleport(targetBucket.nodesBucket);
        }
    }
}
