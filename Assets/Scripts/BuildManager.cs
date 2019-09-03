using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Text bucketText;
    public Text nodeText;
    public Slider loadSlider;

    int numBuckets;
    public int NumBuckets
    {
        get
        {
            return numBuckets;
        }
        set
        {
            numBuckets = value;
            bucketText.text = "Buckets: " + numBuckets;
            loadSlider.value = (numNodes * 1.0f / numBuckets) / loadFactor;
        }
    }
    int numNodes;
    public int NumNodes
    {
        get
        {
            return numNodes;
        }
        set
        {
            numNodes = value;
            nodeText.text = "Nodes: " + numNodes;
            loadSlider.value = (numNodes * 1.0f / numBuckets) / loadFactor;
        }
    }
    public float loadFactor;

    public GameObject bucketAnchor;
    public GameObject bucketPrefab;
    public List<Bucket> buckets;
    Vector2 bucketCursor;

    // how many buckets in each column
    int maxHeight = 10;
    // the width of each bucket
    int bucketWidth = ChainBucket.maxCapacity + 2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        buckets = new List<Bucket>();
        bucketCursor = bucketAnchor.transform.position;
        AddBuckets(1);
        NumNodes = 0;
    }

    public float GetLoadAmount()
    {
        return numNodes * 1.0f / numBuckets;
    }

    // return true if should continue
    public bool CheckLoadFactor()
    {
        if(GetLoadAmount() >= loadFactor)
        {
            RehashBuckets();
            return false;
        }
        return true;
    }

    public void RehashBuckets()
    {
        Rehash.instance.RehashInterrupt();
        // sends all nodes to be rehashed
        foreach (Bucket bucket in buckets)
        {
            bucket.RehashNodes();
        }
        // trigger rehasher to start dispensing
        Invoke("InvokeRehash", 1 / TickManager.tickSpeed * .5f);
        // adds more buckets
        AddBuckets(numBuckets / 2 + 1);
        // orders the buckets nicely
        ReorderBuckets();
    }

    void InvokeRehash()
    {
        Rehash.instance.rehashing = true;
    }

    public void AddBuckets(int num)
    {
        NumBuckets += num;
        for(int j = 0; j < num; j ++)
        {
            ChainBucket bucket = Instantiate(bucketPrefab, bucketCursor, Quaternion.identity).GetComponent<ChainBucket>();
            bucketCursor += Vector2.down;
            bucket.SetBucketIndex(buckets.Count);
            buckets.Add(bucket.gameObject.GetComponent<Bucket>());
        }
    }

    void ReorderBuckets()
    {
        bucketCursor = bucketAnchor.transform.position;
        int sqrt = Mathf.CeilToInt(Mathf.Sqrt(buckets.Count));
        int height = Mathf.Min(sqrt, maxHeight);
        for(int j = 0; j < buckets.Count; j++)
        {
            buckets[j].transform.position = bucketCursor;
            // moves cursor down
            bucketCursor += new Vector2(0, -1);
            // if hit max height, move to the right
            if(bucketAnchor.transform.position.y - bucketCursor.y >= height)
            {
                bucketCursor = new Vector2(bucketCursor.x + bucketWidth, bucketAnchor.transform.position.y);
            }
        }
    }
}
