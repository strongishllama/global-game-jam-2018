﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Up,
    Left,
    Down,
    Right
}

public class CarDrift : MonoBehaviour {

    bool ButtonPressed = false; public void SetButton() { ButtonPressed = true; }
    public float m_Acceleration = 5;
    public float m_MaxSpeed = 4;

    private Vector3 m_Velocity;

    private Direction m_CurrentDirection;

    [Range(0.9f, 1.0f)]
    public float m_VelocityDamp = 0.95f;

    [Range(0.0f, 0.2f)]
    public float m_SmoothVelocityDamp = 0.1f;

    public Direction m_StartingDirection;
    private Vector3 m_StartingPosition;

    void Start() {
        m_CurrentDirection = m_StartingDirection;
        m_StartingPosition = transform.position;
        updateDirection();

        WallCollision wc = GetComponent<WallCollision>();
        if (wc != null) {
            wc.m_LeaveRoadEvent.AddListener(playerOffTracks);
        } else {
            Debug.LogWarning("Player does not have a Wall Collision Script Component");
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 newVel = m_Velocity;
        //velocity damp
        switch (m_CurrentDirection) {
            case Direction.Up:
            case Direction.Down:
                //left right damp
                // newVel.x *= m_VelocityDamp;
                newVel.x = Mathf.SmoothDamp(newVel.x, 0.0f, ref newVel.x, m_SmoothVelocityDamp);
                break;
            case Direction.Left:
            case Direction.Right:
                //up down damp
                newVel.y = Mathf.SmoothDamp(newVel.y, 0.0f, ref newVel.y, m_SmoothVelocityDamp);
                //newVel.y = Mathf.Log(newVel.y) * m_MaxSpeed;
                if(newVel.y < 1) {
                    newVel.y *= m_VelocityDamp;
                }
                print("Vel: "+newVel.y);
                break;
        }
        m_Velocity = newVel;

        m_Velocity += transform.up * m_Acceleration * Time.deltaTime;

        if (m_Velocity.magnitude > m_MaxSpeed) {
            m_Velocity = m_Velocity.normalized * m_MaxSpeed;
        }

        transform.position = transform.position + m_Velocity * Time.deltaTime;

        if (Input.GetButtonDown("Fire1") || ButtonPressed) {
            //dog crap
            switch (m_CurrentDirection) {
                case Direction.Up:
                    m_CurrentDirection = Direction.Left;
                    break;
                case Direction.Left:
                    m_CurrentDirection = Direction.Down;
                    break;
                case Direction.Down:
                    m_CurrentDirection = Direction.Right;
                    break;
                case Direction.Right:
                    m_CurrentDirection = Direction.Up;
                    break;
            }
            ButtonPressed = false;
            updateDirection();
        }
    }

    public void updateDirection(Direction a_Direction) {
        m_CurrentDirection = a_Direction;
        updateDirection();
    }

    public void updateDirection() {
        switch (m_CurrentDirection) {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }

    public void playerOffTracks() {
        m_CurrentDirection = m_StartingDirection;
        transform.position = m_StartingPosition;
        m_Velocity = Vector3.zero;
        updateDirection();
    }


    public void ResetVelocityFromDirection()
    {
        Vector3 newVel = m_Velocity;
        //velocity damp
        switch (m_CurrentDirection)
        {
            case Direction.Up:
            case Direction.Down:
                //left right damp
                newVel.x = 0;
                break;
            case Direction.Left:
            case Direction.Right:
                //up down damp
                newVel.y = 0;
                break;
        }
        m_Velocity = newVel;
    }
}
