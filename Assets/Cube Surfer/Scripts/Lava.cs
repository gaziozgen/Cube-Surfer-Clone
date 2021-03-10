using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private int length = 3;

    public int Length { get => length; }

    void Awake()
    {
        Vector3 scale = transform.localScale;
        scale.z = length;
        transform.localScale = scale;
    }

    void Update()
    {
        
    }
}
