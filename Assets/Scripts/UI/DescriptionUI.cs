using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionUI : MonoBehaviour
{
    public Transform m_Target;
    public Transform m_Point;
    private LineRenderer m_LineRenderer;
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        print(m_Target.position);
    }

    void FixedUpdate()
    {
        if (m_Target!= null && m_Point!= null && m_LineRenderer!= null)
        {
            Vector3 targetPos = m_Target.position;
            // Vector3 screenPoint = m_Camera.WorldToScreenPoint(targetPos);
            Vector3 pointPos = m_Point.position;
            // Vector3 worldPoint = m_Camera.ScreenToWorldPoint(pointPos);
            m_LineRenderer.SetPosition(0, targetPos);
            m_LineRenderer.SetPosition(1, pointPos);
        }
    }
}
