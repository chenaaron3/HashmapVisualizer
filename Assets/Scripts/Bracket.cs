using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracket : Operator
{
    Bucket myBucket;

    private void Start()
    {
        myBucket = transform.GetComponentInParent<Bucket>();
    }

    // request to add to bucket
    public override void OnTick()
    {
        if (node != null)
        {
            if(node.deleter)
            {
                Destroy(node.gameObject);
                Generator.instance.processing = false;
                ViewManager.instance.ResetCamera();
            }
            else
            {
                // checks overflow
                if (myBucket.CheckOverflow())
                {
                    // stops the node
                    node.SetDirection(Vector2.zero);
                    // pushes the bracket over
                    transform.position += transform.right;
                    // if node was generated and not from hash
                    if (node.fromGenerator)
                    {
                        node.fromGenerator = false;
                        // complete process
                        Generator.instance.processing = false;
                        ViewManager.instance.ResetCamera();
                    }
                }
            }
        }
    }
}
