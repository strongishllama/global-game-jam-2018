using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WallCollision : MonoBehaviour {

    public Transform m_WheelHolderPosition;

    [Range(0, 10)]
    public float m_RaycastRange = 5;

    [Range(0, 4)]
    public int m_WheelsAllowedOffRoad = 3;

    public UnityEvent m_LeaveRoadEvent;

    /// <summary>
    /// current state being on road
    /// </summary>
    private bool m_CurrentlyOnRoad = true;

    void Awake() {
        m_LeaveRoadEvent.AddListener(LeaveRoadTest);

        if(m_WheelHolderPosition.childCount == 0) {
            Debug.LogWarning("m_WheelHolderPosition has no children.");
        }
    }

    // Update is called once per frame
    void Update() {

        bool previousOnRoad = m_CurrentlyOnRoad;
        int hits = 0;
        for(int i = 0; i < m_WheelHolderPosition.childCount; i++) {
            if (TestPoint(m_WheelHolderPosition.GetChild(i).position)) {
                hits++;
            }
        }

        m_CurrentlyOnRoad = hits < (m_WheelHolderPosition.childCount - m_WheelsAllowedOffRoad);

        if (m_CurrentlyOnRoad) {
            if (previousOnRoad != m_CurrentlyOnRoad) {
                m_LeaveRoadEvent.Invoke();
            }
        }
    }

    /// <summary>
    /// checks to see if a point is on the road or not
    /// </summary>
    /// <param name="a_Position">Position to test</param>
    /// <returns>false if off the road, true if on the road</returns>
    private bool TestPoint(Vector3 a_Position) {
        //could replace m_Map with the material from the hit transform
        RaycastHit hit;
        Debug.DrawRay(a_Position, Vector3.forward * m_RaycastRange);
        if (Physics.Raycast(a_Position, Vector3.forward, out hit, m_RaycastRange)) {
            Material hitMaterial = hit.transform.GetComponent<MeshRenderer>().material;
            int width = Mathf.FloorToInt(hitMaterial.mainTexture.width * hit.textureCoord.x);
            int height = Mathf.FloorToInt(hitMaterial.mainTexture.height * hit.textureCoord.y);
             
            Color col = ((Texture2D)hitMaterial.mainTexture).GetPixel(width, height);

            float color = col.r + col.g + col.b;

            if (color <= 0.01f) {
                return true;
            }
        }
        return false;
    }

    private void LeaveRoadTest() {
        print("Left Road");
    }
}
