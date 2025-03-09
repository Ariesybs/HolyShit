using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIKDetecter : MonoBehaviour
{
    [SerializeField] List<Transform> m_FootIKTransformsList;
    [SerializeField] List<Transform> m_FootIKTargetList;
    [SerializeField] List<Transform> m_FootIKHintList;
    [SerializeField] Transform m_BodyDetecterTransform;
    [SerializeField] float m_CheckRayStartHeight = 0.5f;
    [SerializeField] float m_CheckRayLength = 1.0f;
    [SerializeField] float m_UpdateFootIKDistance = 0.01f; //超过这个值就更新IK位置
    [SerializeField] float m_FootIKAnimationSpeed = 5f;
    [SerializeField] float m_FootIKStepHeight = 0.05f;
    [SerializeField] float[] m_HitPointOffset;

    private enum DetecteType{
        None,
        Move,
        Rotate
    }
    private DetecteType m_DetecteType;
    private Vector3 m_LastBodyPosition;
    private Vector3[] m_UpdateFootIKPositionsArray;
    private bool m_IsLeft = true;
    private bool[] m_AnimationIsPlaying;

    [Header("Debug")]
    private Vector3[] m_DebugFootsIKPositionsHitArray;
    [SerializeField] float m_DebugSphereRadius = 0.01f;
    void Start()
    {
        m_UpdateFootIKPositionsArray = new Vector3[m_FootIKTransformsList.Count];
        m_DebugFootsIKPositionsHitArray = new Vector3[m_FootIKTransformsList.Count];
        m_AnimationIsPlaying = new bool[m_FootIKTransformsList.Count];

        for(int i = 0; i < m_FootIKTransformsList.Count; i++){
            Transform t = m_FootIKTransformsList[i];
            Vector3 rayStart = t.position + Vector3.up * m_CheckRayStartHeight;
            RaycastHit hit;
            Debug.DrawRay(rayStart, Vector3.down * m_CheckRayLength, Color.blue);
            if (Physics.Raycast(rayStart, Vector3.down, out hit, m_CheckRayLength))
            {
                m_UpdateFootIKPositionsArray[i] = hit.point;
            }
        }

        if (m_BodyDetecterTransform != null){
            RaycastHit hit;
            if (Physics.Raycast(m_BodyDetecterTransform.position, Vector3.down, out hit, m_CheckRayLength))
            {
                m_LastBodyPosition = hit.point;
            }
        }
    }

    void Update()
    {
        CheckFootsIKPositions();
        CheckBodyPosition();
    }

    void CheckBodyPosition(){
        if (m_BodyDetecterTransform != null){
            RaycastHit hit;
            if (Physics.Raycast(m_BodyDetecterTransform.position, Vector3.down, out hit, m_CheckRayLength))
            {
                if(IsDistanceOut(m_LastBodyPosition, hit.point, m_UpdateFootIKDistance)){
                    m_LastBodyPosition = hit.point;
                    UpdateAllFootsPositions();
                }
            }
        }
    }

    void CheckFootsIKPositions()
    {
        for(int i = 0; i < m_FootIKTransformsList.Count; i++){
            Transform t = m_FootIKTransformsList[i];
            Vector3 rayStart = t.position + Vector3.up * m_CheckRayStartHeight;
            Debug.DrawRay(rayStart, Vector3.down * m_CheckRayLength, Color.blue);
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, m_CheckRayLength))
            {
                m_DebugFootsIKPositionsHitArray[i] = hit.point;
                m_FootIKTargetList[i].position = m_UpdateFootIKPositionsArray[i] + m_HitPointOffset[i] * Vector3.up;
            }
        }
    }

    bool IsDistanceOut(Vector3 originalPosition, Vector3 newPosition, float distance){

        return (newPosition - originalPosition).sqrMagnitude > distance * distance;
    }

    void UpdateAllFootsPositions(){
        // print("Update All Foots Positions");
        int[] index;
        if(m_IsLeft){
            index = new int[]{0,3,4};
        }else{
            index = new int[]{1,2,5};
        }
        for(int i = 0; i < index.Length; i++){
            int idx = index[i];
            Transform t = m_FootIKTransformsList[idx];
            Vector3 rayStart = t.position + Vector3.up * m_CheckRayStartHeight;
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, m_CheckRayLength))
            {
                // m_UpdateFootIKPositionsArray[idx] = hit.point;
                if(!m_AnimationIsPlaying[idx])StartCoroutine(HandleFoootIKAniamtion(idx,hit.point));
            }
                
        }

        m_IsLeft =!m_IsLeft;
    }

    void UpdateFootsPositions(int footIndex, Vector3 newPosition){
        // print("Update Foots Positions");
        // m_FootIKTargetList[footIndex].position = newPosition + m_HitPointOffset[footIndex] * Vector3.up;
        // StartCoroutine(HandleFoootIKAniamtion(m_FootIKTargetList[footIndex],newPosition + m_HitPointOffset[footIndex] * Vector3.up));
    }

    IEnumerator HandleFoootIKAniamtion(int footIndex, Vector3 newPosition){
        m_AnimationIsPlaying[footIndex] = true;
        float lerp = 0;
        Transform targetIK = m_FootIKTargetList[footIndex];
        Vector3 oldPosition = targetIK.position;
        Vector3 pos;
        while(lerp < 1){
            lerp += Time.deltaTime * m_FootIKAnimationSpeed;
            pos = Vector3.Lerp(oldPosition, newPosition, lerp);
            pos.y += Mathf.Sin(lerp * Mathf.PI) * m_FootIKStepHeight;
            m_UpdateFootIKPositionsArray[footIndex] = pos;
            yield return null;
        }
        m_AnimationIsPlaying[footIndex] = false;
    }

    void OnDrawGizmos(){
        if(m_DebugFootsIKPositionsHitArray == null)return;
        Gizmos.color = Color.green;
        for(int i = 0; i < m_DebugFootsIKPositionsHitArray.Length; i++){
            Gizmos.DrawSphere(m_DebugFootsIKPositionsHitArray[i], m_DebugSphereRadius);
        }
        Gizmos.DrawSphere(m_LastBodyPosition, m_DebugSphereRadius);
    }
}
