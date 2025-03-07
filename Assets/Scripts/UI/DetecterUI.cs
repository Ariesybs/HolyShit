using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecterUI : MonoBehaviour
{
    public Transform m_Target;
    public RectTransform m_Point;
    public Camera m_Camera;
    private RectTransform m_RectTransform;
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_Camera != null && m_Target!= null && m_Point!= null){
            Vector3 viewportPos = m_Camera.WorldToViewportPoint(m_Target.position);
            // m_Point.anchoredPosition = uiPos;
            Vector3 screenPos = m_Camera.ViewportToScreenPoint(viewportPos);
            Vector3 uiPos = screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0);
            m_Point.anchoredPosition = uiPos;
            // Vector3 uiPos = m_RectTransform.InverseTransformPoint(m_Camera.ViewportToScreenPoint(pos));
            // m_Point.position = uiPos;
        }
    }
}
