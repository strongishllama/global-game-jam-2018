using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public float Acceleration;
    float Speed;
    int z;
    int PlayerNumber;
    public float SpeedLostOnTurn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Speed += Acceleration * Time.deltaTime;
        transform.position += transform.up * Speed * Time.deltaTime;

       if (Input.GetButtonDown("Fire1"))
        {
            if (z == 270)
                z = 0;
            else
                z += 90;

            Speed -= SpeedLostOnTurn;
            transform.rotation = Quaternion.Euler(0, 0, z);

        }
        

        
    }
}
