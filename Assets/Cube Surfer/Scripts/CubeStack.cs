using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStack : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab = null;
    [SerializeField] private GameObject cubePrefab = null;
    [SerializeField] private int startCubeNumber = 2;
    private GameObject character;
    private Queue<Cube> stack;


    void Awake()
    {
        stack = new Queue<Cube>();

        for (int i = 0; i < startCubeNumber; i++)
        {
            stack.Enqueue(Instantiate(cubePrefab, new Vector3(0, i, 0), Quaternion.Euler(0, 0, 0), transform).GetComponent<Cube>());
        }

        character = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), transform);
        SetCharacterPosition();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddCubes(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoseCubes(1);
        }
    }

    private void AddCubes(int n)
    {
        int c = stack.Count;
        for (int i = c; i < (c+n); i++)
        {
            Cube cube = Instantiate(cubePrefab, new Vector3(transform.position.x, i, transform.position.z), Quaternion.Euler(0, 0, 0), transform).GetComponent<Cube>();
            stack.Enqueue(cube);
            print(cube.transform.position);
            print(cube.transform.localPosition);
        }
        SetCharacterPosition();
    }

    private void LoseCubes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Cube cube = stack.Dequeue();
            Destroy(cube.gameObject);
        }
        SetCharacterPosition();
    }


    private void SetCharacterPosition()
    {
        Vector3 pos = character.transform.localPosition;
        pos.y = stack.Count;
        character.transform.localPosition = pos;
    }

}
