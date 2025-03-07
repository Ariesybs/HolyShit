using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitController : MonoBehaviour
{
    private Rigidbody m_RB;
    private Camera m_Camera;
    [SerializeField] float m_Speed = 10f;
    void Start()
    {
        m_Camera = Camera.main;
        m_RB = GetComponent<Rigidbody>();
    }
    
        
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = m_Camera.transform.right * moveHorizontal + m_Camera.transform.forward * moveVertical;
        m_RB.AddForce(movement * m_Speed * Time.deltaTime  , ForceMode.Force);
        m_RB.velocity = Vector3.ClampMagnitude(m_RB.velocity, 0.8f);

    }
}
