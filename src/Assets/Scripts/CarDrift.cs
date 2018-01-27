using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrift : MonoBehaviour {

    enum Direction {
        Up,
        Left,
        Down,
        Right
    }

    public float m_Acceleration = 5;

    private Vector3 m_Velocity;

    private Direction m_CurrentDirection;

    [Range(0.5f,1.0f)]
    public float m_VelocityDamp = 0.95f;
	
	// Update is called once per frame
	void Update () {
        m_Velocity *= m_VelocityDamp;

        m_Velocity += transform.up * m_Acceleration * Time.deltaTime;

        transform.position = transform.position + m_Velocity * Time.deltaTime;

        if (Input.GetButtonDown("Fire1")) {
            switch (m_CurrentDirection) {
                case Direction.Up:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    m_CurrentDirection = Direction.Left;
                    break;
                case Direction.Left:
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    m_CurrentDirection = Direction.Down;
                    break;
                case Direction.Down:
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    m_CurrentDirection = Direction.Right;
                    break;
                case Direction.Right:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    m_CurrentDirection = Direction.Up;
                    break;
            }
        }
    }
}
