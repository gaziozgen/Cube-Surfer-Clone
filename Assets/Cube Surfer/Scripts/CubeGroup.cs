using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGroup : MonoBehaviour
{
    [SerializeField] private GameObject CubekPrefab = null;
    public int height = 1;

    void Awake()
    {
        for (int i = 0; i < height; i++)
        {
            Instantiate(CubekPrefab, new Vector3(transform.position.x, i, transform.position.z), Quaternion.Euler(0, 0, 0), transform);
        }
    }

    void Update()
    {
        
    }
}
