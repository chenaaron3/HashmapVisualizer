using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rehash : MonoBehaviour
{
    public static Rehash instance;

    public GameObject spawn;
    public List<Node> queue;

    public bool rehashing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        queue = new List<Node>();
        rehashing = false;
    }

    private void OnEnable()
    {
        TickManager.OnTick += OnTick;
    }

    private void OnDisable()
    {
        TickManager.OnTick -= OnTick;
    }

    void OnTick()
    {
        if(rehashing)
        {
            if (queue.Count > 0)
            {
                Node node = queue[0];
                queue.RemoveAt(0);
                node.SetDirection(transform.right * -1);
            }
            else
            {
                rehashing = false;
            }
        }
    }

    public void RehashNode(Node node)
    {
        print(node);
        node.transform.parent = null;
        node.transform.position = spawn.transform.position;
        node.SetDirection(Vector2.zero);
        queue.Add(node);
    }
}
