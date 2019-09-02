using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainBucket : Bucket
{
    public static int maxCapacity = 5;

    public Animator pushAnim;
    public Operator bracket;
    public Text bucketIndex;

    private void Start()
    {
        pushAnim.speed = TickManager.tickSpeed;
    }

    public override void OnTick()
    {
        // triggers pin to push
        pushAnim.SetTrigger("push");
        if (node != null)
        {
            // starts to chain the nodes
            node.SetDirection(transform.right);
        }
    }

    // returns true if should continue
    public override bool CheckOverflow()
    {
        // if too many children
        if (nodesBucket.transform.childCount >= maxCapacity)
        {
            BuildManager.instance.RehashBuckets();
            return false;
        }
        return true;
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
