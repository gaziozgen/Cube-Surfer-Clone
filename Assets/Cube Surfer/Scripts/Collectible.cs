using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateGames;

public class Collectible : MonoBehaviour
{
    private CubeSurferLevel levelManager;

    void Awake()
    {
        levelManager = (CubeSurferLevel)LevelManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        CubeStack cubeStack = other.transform.GetComponent<CubeStack>();
        if (cubeStack)
        {
            Collected();
        }
    }

    private void Collected()
    {
        levelManager.AddScore(1);
        transform.LeanMoveLocalY(2f, 0.3f);
        transform.gameObject.SetActive(false);
    }

}
