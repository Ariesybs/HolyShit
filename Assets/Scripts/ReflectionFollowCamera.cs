using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionFollowCamera : MonoBehaviour
{
    private Camera m_Camera;
    void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = m_Camera.transform.position;
        pos.y = -pos.y;
        transform.position = pos;
    }
}
