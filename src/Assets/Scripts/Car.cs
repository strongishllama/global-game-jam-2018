using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public float Acceleration;
    float Speed;
    int z;
    int PlayerNumber;
    public float SpeedLostOnTurn;
    Vector3 Velocity;
    public float Drag;
    Vector3 DragV3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Speed += Acceleration * Time.deltaTime;
        Velocity += transform.up * Speed * Time.deltaTime;
        transform.position += Velocity * Time.deltaTime ;
        Velocity -= DragV3;
        DragV3 = new Vector3(Drag * Time.deltaTime, Drag * Time.deltaTime, 0);
        
       if (Input.GetButtonDown("Fire1"))
        {
            if (z == 270)
                z = 0;
            else
                z += 90;

            Speed *= SpeedLostOnTurn;
            transform.rotation = Quaternion.Euler(0, 0, z);

        }
        

        
    }
}
