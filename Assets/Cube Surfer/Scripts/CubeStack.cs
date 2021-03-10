using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateGames;

public class CubeStack : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab = null;
    [SerializeField] private GameObject cubePrefab = null;
    [SerializeField] private int startCubeNumber = 2;
    private GameObject character;
    private Queue<Cube> stack;
    private CubeSurferLevel levelManager;
    private bool lavaDamage = true;
    private int cubelose = 0;


    void Awake()
    {
        levelManager = (CubeSurferLevel)LevelManager.Instance;
        stack = new Queue<Cube>();
        for (int i = 0; i < startCubeNumber-1; i++)
        {
            stack.Enqueue(Instantiate(cubePrefab, new Vector3(0, i, 0), Quaternion.Euler(0, 0, 0), transform).GetComponent<Cube>());
        }
        character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), transform);
        SetCharacterPosition();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddCubes(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoseCubes(1, true);
        }

        if (lavaDamage)
        {
            LavaCheck();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        PlusCubeCheck(other);
        BlockCheck(other);
    }

    private void PlusCubeCheck(Collider other)
    {
        CubeGroup cube = other.GetComponent<CubeGroup>();
        if (cube)
        {
            AddCubes(cube.Height);
            Destroy(other.gameObject);
        }
    }

    private void BlockCheck(Collider other)
    {
        BlockGroup block = other.GetComponent<BlockGroup>();
        if (block)
        {
            LoseCubes(block.Height, false);
        }
    }

    private void LavaCheck()
    {
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        pos.z += 0.5f;

        if (Physics.Raycast(pos, -Vector3.up, out RaycastHit hit, 0.46f))
        {
            Lava lava = hit.transform.GetComponent<Lava>();
            if (lava)
            {
                lavaDamage = false;
                LoseCubes(1, true);
                LeanTween.delayedCall(0.4f, () =>
                {
                    lavaDamage = true;
                });
            }
        }
    }

    private void AddCubes(int n)
    {
        int c = stack.Count;
        for (int i = c; i < (c+n); i++)
        {
            Cube cube = Instantiate(cubePrefab, new Vector3(transform.position.x, i, transform.position.z), Quaternion.Euler(0, 0, 0), transform).GetComponent<Cube>();
            stack.Enqueue(cube);
        }
        SetCharacterPosition();
    }

    private void LoseCubes(int n, bool delete)
    {
        if (delete)
        {
            CheckLose(n);
            Cube cube = stack.Dequeue();
            if (delete)
            {
                Destroy(cube.gameObject);
            }
        }
        else if (n > cubelose)
        {

            n -= cubelose;
            CheckLose(n);
            for (int i = 0; i < n; i++)
            {
                Cube cube = stack.Dequeue();
                cube.transform.SetParent(null);
            }
            cubelose = n + cubelose;
            LeanTween.delayedCall(1.5f, () =>
            {
                cubelose = 0;
            });
        }
    }

    private void CheckLose(int n)
    {
        if (n > stack.Count)
        {
            levelManager.FinishLevel(false);
        }
    }


    private void SetCharacterPosition()
    {
        Vector3 pos = character.transform.localPosition;
        pos.y = stack.Count;
        character.transform.localPosition = pos;
    }
}
