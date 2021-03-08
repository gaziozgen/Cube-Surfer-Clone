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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddCubes(3);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoseCubes(2);
        }
        
        Vector3 pos = transform.position;
        pos.y = 0.5f;
        Debug.DrawRay(pos, Vector3.forward, Color.red, 5f);
        if (Physics.Raycast(pos, Vector3.forward, out RaycastHit hit, 1.1f))
        {
            print(hit.transform.position);
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

    private void LoseCubes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Cube cube = stack.Dequeue();
            Destroy(cube.gameObject);
        }
    }


    private void SetCharacterPosition()
    {
        Vector3 pos = character.transform.localPosition;
        pos.y = stack.Count;
        character.transform.localPosition = pos;
    }

}
