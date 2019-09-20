using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexGizmo : MonoBehaviour
{
    private MeshFilter filter;
    public float gizmoRadius;
    
    void Start()
    {
        filter = GetComponent<MeshFilter>();
    }
    
    private void OnDrawGizmos()
    {
        if (filter != null)
        {
            for (int i = 0; i < filter.mesh.vertices.Length; i++)
            {
                Gizmos.DrawSphere(transform.TransformPoint(filter.mesh.vertices[i]), gizmoRadius);
            }
        }
    }
}