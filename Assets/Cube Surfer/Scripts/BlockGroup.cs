using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab = null;
    [SerializeField] private int height = 1;

    public int Height { get => height; }

    void Awake()
    {
        for (int i = 0; i < height; i++)
        {
            Instantiate(blockPrefab, new Vector3(transform.position.x, i, transform.position.z), Quaternion.Euler(0, 0, 0), transform);
        }
    }

    void Update()
    {
        
    }


}
