using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WallCollision : MonoBehaviour {

    //public List<Transform> m_WheelPositions;

    public Material m_Map;

    [Range(0, 10)]
    public float m_RaycastRange = 5;

    public UnityEvent m_LeaveRoadEvent;

    /// <summary>
    /// current state being on road
    /// </summary>
    private bool m_CurrentlyOnRoad = true;

    void Awake() {
        m_LeaveRoadEvent.AddListener(LeaveRoadTest);
    }

    // Update is called once per frame
    void Update() {

        bool previousOnRoad = m_CurrentlyOnRoad;
        m_CurrentlyOnRoad = TestPoint(transform.position);

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
    /// <returns>true if off the road, false if on the road</returns>
    private bool TestPoint(Vector3 a_Position) {
        //could replace m_Map with the material from the hit transform
        RaycastHit hit;
        Debug.DrawRay(a_Position, Vector3.down * m_RaycastRange);
        if (Physics.Raycast(a_Position, Vector3.down, out hit, m_RaycastRange)) {
            int width = Mathf.FloorToInt(m_Map.mainTexture.width * hit.textureCoord.x);
            int height = Mathf.FloorToInt(m_Map.mainTexture.height * hit.textureCoord.y);
             
            Color col = ((Texture2D)m_Map.mainTexture).GetPixel(width, height);

            float color = col.r + col.g + col.b;

            if (color <= 0.01f) {
                return false;
            }
        }
        return true;
    }

    private void LeaveRoadTest() {
        print("Left Road");
    }
}
