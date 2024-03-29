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
    [Range(0.0f, -1.0f)]
    public float m_BounceForce;
    bool ButtonPressed = false; public void SetButton() { ButtonPressed = true; }
    public bool m_IsNetworkedCar = false; public void SetNetworked() { m_IsNetworkedCar = true; }
    public float m_Acceleration = 5;
    public float m_MaxSpeed = 4;

    private Vector3 m_Velocity;

    private Direction m_CurrentDirection;

    [Range(0.9f, 1.0f)]
    public float m_VelocityDamp = 0.95f;

    [Range(0.0f, 0.2f)]
    public float m_SmoothVelocityDamp = 0.1f;

    [Range(0.0f, 1.0f)]
    public float m_SpeedMovedFromTurn = 0.5f;
    
    private Vector3 m_StartingPosition;
    private Vector3 m_StartingRotation;

    private int m_CurrentLapCount = 0;
    
    public int m_health = 3;
    [SerializeField]
    private GameObject carExplosion;

    [SerializeField]
    private List<AudioClip> carExplosions = new List<AudioClip>();

    void Start() {
        WallCollision wc = GetComponent<WallCollision>();
        if (wc != null) {
            wc.m_LeaveRoadEvent.AddListener(playerOffTracks);
        } else {
            Debug.LogWarning("Player does not have a Wall Collision Script Component");
        }

        transform.position = transform.position + new Vector3(0, 0, -0.1f);

        m_CurrentDirection = LevelManager.instance.GetCurrentLevel().dir;
        m_StartingPosition = transform.position;
        m_StartingRotation = transform.rotation.eulerAngles;
        updateDirection();



    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.GameState == eGameState.PlayingGame)
        {
            Vector3 newVel = m_Velocity;
            Vector3 speedTaken = newVel;

            //velocity damp
            switch (m_CurrentDirection)
            {
                case Direction.Up:
                case Direction.Down:
                    //left right damp
                    // newVel.x *= m_VelocityDamp;
                    newVel.x = Mathf.SmoothDamp(newVel.x, 0.0f, ref newVel.x, m_SmoothVelocityDamp);
                    if (Mathf.Abs(newVel.x) < 1.5f)
                    {
                        newVel.x *= .95f;
                    }
                    break;
                case Direction.Left:
                case Direction.Right:
                    //up down damp
                    newVel.y = Mathf.SmoothDamp(newVel.y, 0.0f, ref newVel.y, m_SmoothVelocityDamp);
                    //newVel.y = Mathf.Log(newVel.y) * m_MaxSpeed;
                    if (Mathf.Abs(newVel.y) < 1.5f)
                    {
                        newVel.y *= .95f;
                    }
                    break;
            }

            m_Velocity = newVel;
            speedTaken -= newVel;

            {
                float tempY = speedTaken.y;
                speedTaken.y = speedTaken.x;
                speedTaken.x = -tempY;
                speedTaken *= m_SpeedMovedFromTurn;

            }

            m_Velocity += transform.up * m_Acceleration * Time.deltaTime + speedTaken;

            if (m_Velocity.magnitude > m_MaxSpeed)
            {
                m_Velocity = m_Velocity.normalized * m_MaxSpeed;
            }

            transform.position = transform.position + m_Velocity * Time.deltaTime;
        }

        bool keyPress = false;
        if (!m_IsNetworkedCar) {
            keyPress |= Input.GetButtonDown("Fire1");
            keyPress |= Input.GetKeyDown(KeyCode.J);
        }

        if (keyPress || ButtonPressed) {
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
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;                                 
            case Direction.Left:                        
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;                                   
            case Direction.Down:                         
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;                                    
            case Direction.Right:                         
                transform.localRotation = Quaternion.Euler(0, 0, 270);
                break;
        }
        //switch (m_CurrentDirection) {
        //    case Direction.Up:
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, m_StartingRotation.y, 0);
        //        break;
        //    case Direction.Left:
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, m_StartingRotation.y, 90);
        //        break;
        //    case Direction.Down:
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, m_StartingRotation.y, 180);
        //        break;
        //    case Direction.Right:
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, m_StartingRotation.y, 270);
        //        break;
        //}
        //switch (m_CurrentDirection) {
        //    case Direction.Up:
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, 0  , m_StartingRotation.z);
        //        break;                                                               
        //    case Direction.Left:                                                     
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, 90 , m_StartingRotation.z);
        //        break;                                                              
        //    case Direction.Down:                                                    
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, 180, m_StartingRotation.z);
        //        break;                                                              
        //    case Direction.Right:                                                    
        //        transform.localRotation = Quaternion.Euler(m_StartingRotation.x, 270, m_StartingRotation.z);
        //        break;
        //}
    }

    public void playerOffTracks() {

        if (m_health > 0)
        {
            m_health--;
            int[] Wheels = GetComponent<WallCollision>().GetWheelTable();

            Vector3 test;
                float BounceVelocity;
                if (Wheels[0] == 1 && Wheels[2] == 1)
                {//front
                    
                    switch (m_CurrentDirection)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            BounceVelocity = m_Velocity.y * m_BounceForce;
                       test = new Vector3(m_Velocity.x, BounceVelocity, 0);
                            m_Velocity = test;
                            Debug.Log("Front up");
                            break;

                        case Direction.Left:
                        case Direction.Right:
                            BounceVelocity = m_Velocity.x * m_BounceForce;
                            test = new Vector3(BounceVelocity, m_Velocity.y, 0);
                            m_Velocity = test;
                            Debug.Log("Front right");
                            break;
                    }
                }
                else if (Wheels[0] == 1 && Wheels[1] == 1)
                {//left
                    switch (m_CurrentDirection)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            BounceVelocity = m_Velocity.x * m_BounceForce;
                        test = new Vector3(BounceVelocity, m_Velocity.y, 0);
                        m_Velocity = test;
                        Debug.Log("left up");
                        break;

                        case Direction.Left:
                        case Direction.Right:
                            BounceVelocity = m_Velocity.y * m_BounceForce;
                        test = new Vector3(m_Velocity.x, BounceVelocity, 0);
                        m_Velocity = test;
                        Debug.Log("left right");
                        break;
                    }
                }
                else if (Wheels[2] == 1 && Wheels[3] == 1)
                {//right
                    switch (m_CurrentDirection)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            BounceVelocity = m_Velocity.x * m_BounceForce;
                        test = new Vector3(BounceVelocity, m_Velocity.y, 0);
                        m_Velocity = test;
                        Debug.Log("right up");
                        break;

                        case Direction.Left:
                        case Direction.Right:
                            BounceVelocity = m_Velocity.y * m_BounceForce;
                        test = new Vector3(m_Velocity.x, BounceVelocity, 0);
                        m_Velocity = test;
                        Debug.Log("right right");
                        break;
                    }
                }
                else if (Wheels[1] == 1 && Wheels[3] == 1)
                {//back
                    switch (m_CurrentDirection)
                    {
                        case Direction.Up:
                        case Direction.Down:
                            BounceVelocity = m_Velocity.y * m_BounceForce;
                        test =  new Vector3(m_Velocity.x, BounceVelocity, 0);
                        m_Velocity = test;
                        Debug.Log("back up");
                        break;

                        case Direction.Left:
                        case Direction.Right:
                            BounceVelocity = m_Velocity.x * m_BounceForce;
                        test = new Vector3(BounceVelocity, m_Velocity.y, 0);
                        m_Velocity = test;
                        Debug.Log("back right");
                        break;
                    }
                }

            
        }


        else
        {
            if (carExplosion != null)
            {
                GameObject explosion = Instantiate(carExplosion, transform.position, transform.rotation);
                Destroy(explosion, 4.0f);
            }
            else
            {
                Debug.LogWarning("Car Explosion missing");
            }

            Camera.main.GetComponent<AudioSource>().PlayOneShot(carExplosions[Random.Range(0, carExplosions.Count)]);
            m_CurrentDirection = LevelManager.instance.m_levelinfo[0].dir;
            transform.position = m_StartingPosition;
            m_Velocity = Vector3.zero;
            updateDirection();
        }
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

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<TrackStart>() != null) {
            m_CurrentLapCount++;
            if(m_CurrentLapCount == 4) {
                FindObjectOfType<UICanvas>().winrar();
                gameObject.SetActive(true);
            }
        }
    }

}
