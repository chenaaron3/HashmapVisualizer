using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainBucket : Bucket
{
    public Animator pushAnim;
    public Operator bracket;
    public Text bucketIndex;

    public override void OnTick()
    {
        if (node != null)
        {
            // triggers pin to push
            pushAnim.SetTrigger("push");
            // starts to chain the nodes
            node.SetDirection(transform.right);
        }
    }

    public void SetBucketIndex(int index)
    {
        bucketIndex.text = index + "";
    }

    public override void RehashNodes()
    {
        // sends all nodes to rehash
        while(nodesBucket.transform.childCount != 0)
        {
            Transform child = nodesBucket.transform.GetChild(0);
            Rehash.instance.RehashNode(child.GetComponent<Node>());
        }
        // resets bracket
        bracket.transform.position = transform.position + transform.right;
    }
}
