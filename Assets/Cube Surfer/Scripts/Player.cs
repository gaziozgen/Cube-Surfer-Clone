using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateGames;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    private Swerve1D swerve;
    private float swerveStart = 0;


    void Awake()
    {
        swerve = InputManager.CreateSwerve1D(Vector2.right);
        swerve.OnStart = () => { swerveStart = transform.position.x; };
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.STARTED)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 pos = transform.position;
        if (swerve.Active)
        {
            float x = (swerve.Rate * 4f) + swerveStart;
            pos.x = Mathf.Clamp(x, -2f, 2f);
        }
        pos.z += Time.deltaTime * speed;
        transform.position = pos;
    }
}
