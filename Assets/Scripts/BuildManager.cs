using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public int numBuckets;
    public int numNodes;
    public float loadFactor;

    public GameObject bucketAnchor;
    public GameObject bucketPrefab;
    public List<Bucket> buckets;
    Vector2 bucketCursor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        buckets = new List<Bucket>();
        bucketCursor = bucketAnchor.transform.position;
        AddBuckets(1);
        numNodes = 0;
    }

    public float GetLoadAmount()
    {
        return numNodes * 1.0f / numBuckets;
    }

    public bool CheckLoadFactor()
    {
        if(GetLoadAmount() >= loadFactor)
        {
            Rehash.instance.rehashing = true;
            // sends all nodes to be rehashed
            foreach(Bucket bucket in buckets)
            {
                bucket.RehashNodes();
            }
            // adds more buckets
            AddBuckets(numBuckets + 1);
            return false;
        }
        return true;
    }

    public void AddBuckets(int num)
    {
        numBuckets += num;
        for(int j = 0; j < num; j ++)
        {
            ChainBucket bucket = Instantiate(bucketPrefab, bucketCursor, Quaternion.identity).GetComponent<ChainBucket>();
            bucketCursor += Vector2.down;
            bucket.SetBucketIndex(buckets.Count);
            buckets.Add(bucket.gameObject.GetComponent<Bucket>());
        }
    }
}
