using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonGenerator : MonoBehaviour
{
    private Mesh customMesh;

    public enum PrimitiveShape { QUAD, CUBE, NGON }
    public PrimitiveShape shape;

    public int ngonSides;
    public float ngonRadius;

    public float quadWidth;
    public float quadHeight;

    void Start()
    {
        switch(shape)
        {
            case PrimitiveShape.QUAD:
                customMesh = Create2DQuad(quadWidth, quadHeight);
                break;
            case PrimitiveShape.CUBE:
                customMesh = Create3DCube();
                break;
            case PrimitiveShape.NGON:
                customMesh = Create2DNgon(ngonRadius, ngonSides);
                break;
        }

        var filter = GetComponent<MeshFilter>();
        filter.mesh = customMesh;
    }

    private void OnValidate()
    {
        switch (shape)
        {
            case PrimitiveShape.QUAD:
                customMesh = Create2DQuad(quadWidth, quadHeight);
                break;
            case PrimitiveShape.CUBE:
                customMesh = Create3DCube();
                break;
            case PrimitiveShape.NGON:
                customMesh = Create2DNgon(ngonRadius, ngonSides);
                break;
        }

        var filter = GetComponent<MeshFilter>();
        filter.mesh = customMesh;
    }

    Mesh Create2DQuad(float width, float height)
    {
        //Create a new Mesh asset
        var mesh = new Mesh();

        //Create the vertices of the mesh
        var verts = new Vector3[4];
        verts[0] = new Vector3(0.5f  * width, -0.5f * height, 0);
        verts[1] = new Vector3(-0.5f * width, -0.5f * height, 0);
        verts[2] = new Vector3(-0.5f * width,  0.5f * height, 0);
        verts[3] = new Vector3(0.5f  * width,  0.5f * height, 0);
        //Assign these values back to the mesh
        mesh.vertices = verts;

        //Create the indices to be used to create the mesh's tris
        var indices = new int[6];
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;
        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;
        //Assign the indices back to the mesh
        mesh.triangles = indices;

        //Create the normals on the vertex level
        var norms = new Vector3[4];
        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;
        //Assign the normals back to the mesh
        mesh.normals = norms;

        //Create the UVs for the mesh
        var UVs = new Vector2[4];
        UVs[0] = new Vector2(1, 0);
        UVs[1] = new Vector2(0, 0);
        UVs[2] = new Vector2(0, 1);
        UVs[3] = new Vector2(1, 1);
        //Assign the UVs back to the mesh
        mesh.uv = UVs;

        return mesh;
    }

    Mesh Create2DNgon(float radius, int sides)
    {
        //Create a new Mesh asset
        var mesh = new Mesh();

        //Create the vertices of the mesh
        var verts = new Vector3[sides];
        //Calculate the positions of each vertex
        for (int i = 0; i < verts.Length; i++)
            verts[verts.Length - 1 - i] = new Vector3(radius * -Mathf.Cos(2 * Mathf.PI * i / sides), radius * -Mathf.Sin(2 * Mathf.PI * i / sides));
        //Assign these values back to the mesh
        mesh.vertices = verts;

        //Create the indices to be used to create the mesh's tris
        var indices = new int[3 * (sides - 2)];
        //Calculate the indices
        int iterator = 0;
        for(int i = 0; i < indices.Length; i += 3)
        {
            indices[i] = 0;
            indices[i + 1] = iterator + 1;
            indices[i + 2] = iterator + 2;
            iterator++;
        }
        //Assign the indices back to the mesh
        mesh.triangles = indices;

        //Create the normals on the vertex level
        var norms = new Vector3[verts.Length];
        //Set all the normals to negative forward
        for (int i = 0; i < verts.Length; i++)
            norms[i] = -Vector3.forward;
        //Assign the normals back to the mesh
        mesh.normals = norms;

        //Create the UVs for the mesh
        var UVs = new Vector2[sides];
        //Set the UVs to be the same as the verts
        for(int i = 0; i < sides; i++)
            UVs[i] = verts[i];
        //Assign the UVs back to the mesh
        mesh.uv = UVs;

        return mesh;
    }

    Mesh Create3DCube()
    {
        //Create a new Mesh asset
        var mesh = new Mesh();

        //Create the vertices of the mesh
        var verts = new Vector3[24];
        //Front
        verts[0] = new Vector3(-0.5f, -0.5f, -0.5f);
        verts[1] = new Vector3(-0.5f, 0.5f, -0.5f);
        verts[2] = new Vector3(0.5f, 0.5f, -0.5f);
        verts[3] = new Vector3(0.5f, -0.5f, -0.5f);
        //Right
        verts[4] = new Vector3(0.5f, -0.5f, -0.5f);
        verts[5] = new Vector3(0.5f, 0.5f, -0.5f);
        verts[6] = new Vector3(0.5f, 0.5f, 0.5f);
        verts[7] = new Vector3(0.5f, -0.5f, 0.5f);
        //Back
        verts[8]  = new Vector3(0.5f, -0.5f, 0.5f);
        verts[9]  = new Vector3(0.5f, 0.5f, 0.5f);
        verts[10] = new Vector3(-0.5f, 0.5f, 0.5f);
        verts[11] = new Vector3(-0.5f, -0.5f, 0.5f);
        //Left
        verts[12] = new Vector3(-0.5f, -0.5f, 0.5f);
        verts[13] = new Vector3(-0.5f, 0.5f, 0.5f);
        verts[14] = new Vector3(-0.5f, 0.5f, -0.5f);
        verts[15] = new Vector3(-0.5f, -0.5f, -0.5f);
        //Top
        verts[16] = new Vector3(-0.5f, 0.5f, -0.5f);
        verts[17] = new Vector3(-0.5f, 0.5f, 0.5f);
        verts[18] = new Vector3(0.5f, 0.5f, 0.5f);
        verts[19] = new Vector3(0.5f, 0.5f, -0.5f);
        //Bottom
        verts[20] = new Vector3(-0.5f, -0.5f, 0.5f);
        verts[21] = new Vector3(-0.5f, -0.5f, -0.5f);
        verts[22] = new Vector3(0.5f, -0.5f, -0.5f);
        verts[23] = new Vector3(0.5f, -0.5f, 0.5f);

        //Assign these values back to the mesh
        mesh.vertices = verts;

        //Create the indices to be used to create the mesh's tris
        var indices = new int[36];
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;

        indices[3] = 2;
        indices[4] = 3;
        indices[5] = 0;

        indices[6] = 4;
        indices[7] = 5;
        indices[8] = 6;

        indices[9]  = 6;
        indices[10] = 7;
        indices[11] = 4;

        indices[12] = 8;
        indices[13] = 9;
        indices[14] = 10;

        indices[15] = 10;
        indices[16] = 11;
        indices[17] = 8;

        indices[18] = 12;
        indices[19] = 13;
        indices[20] = 14;

        indices[21] = 14;
        indices[22] = 15;
        indices[23] = 12;

        indices[24] = 16;
        indices[25] = 17;
        indices[26] = 18;

        indices[27] = 18;
        indices[28] = 19;
        indices[29] = 16;

        indices[30] = 20;
        indices[31] = 21;
        indices[32] = 22;

        indices[33] = 22;
        indices[34] = 23;
        indices[35] = 20;
        //Assign the indices back to the mesh
        mesh.triangles = indices;

        //Create the normals on the vertex level
        var norms = new Vector3[24];
        norms[0] = Vector3.back;
        norms[1] = Vector3.back;
        norms[2] = Vector3.back;
        norms[3] = Vector3.back;

        norms[4] = Vector3.right;
        norms[5] = Vector3.right;
        norms[6] = Vector3.right;
        norms[7] = Vector3.right;

        norms[8]  = Vector3.forward;
        norms[9]  = Vector3.forward;
        norms[10] = Vector3.forward;
        norms[11] = Vector3.forward;

        norms[12] = Vector3.left;
        norms[13] = Vector3.left;
        norms[14] = Vector3.left;
        norms[15] = Vector3.left;

        norms[16] = Vector3.up;
        norms[17] = Vector3.up;
        norms[18] = Vector3.up;
        norms[19] = Vector3.up;

        norms[20] = Vector3.down;
        norms[21] = Vector3.down;
        norms[22] = Vector3.down;
        norms[23] = Vector3.down;
        //Assign the normals back to the mesh
        mesh.normals = norms;

        //Create the UVs for the mesh
        var UVs = new Vector2[24];
        UVs[0] = new Vector2(1, 1);
        UVs[1] = new Vector2(1, 2);
        UVs[2] = new Vector2(2, 2);
        UVs[3] = new Vector2(2, 1);

        UVs[4] = new Vector2(3, 2);
        UVs[5] = new Vector2(2, 2);
        UVs[6] = new Vector2(2, 3);
        UVs[7] = new Vector2(3, 3);

        UVs[8] = new Vector2(2, 4);
        UVs[9] = new Vector2(2, 3);
        UVs[10] = new Vector2(1, 3);
        UVs[11] = new Vector2(1, 4);

        UVs[12] = new Vector2(0, 3);
        UVs[13] = new Vector2(1, 3);
        UVs[14] = new Vector2(1, 2);
        UVs[15] = new Vector2(0, 2);

        UVs[16] = new Vector2(1, 2);
        UVs[17] = new Vector2(1, 3);
        UVs[18] = new Vector2(2, 3);
        UVs[19] = new Vector2(2, 2);

        UVs[20] = new Vector2(1, 0);
        UVs[21] = new Vector2(1, 1);
        UVs[22] = new Vector2(2, 1);
        UVs[23] = new Vector2(2, 0);
        //Assign the UVs back to the mesh
        mesh.uv = UVs;

        return mesh;
    }
    /*
    Mesh Create3DCylinder(float radius, int sides, float height)
    {
        //Create a new Mesh asset
        var mesh = new Mesh();

        //Create the vertices of the mesh
        var verts = new Vector3[sides * 2];
        //Calculate the positions of each vertex
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = new Vector3(radius * -Mathf.Cos(2 * Mathf.PI * i / columns), radius * -Mathf.Sin(2 * Mathf.PI * i / columns));
        }
        //Assign these values back to the mesh
        mesh.vertices = verts;

        //Create the indices to be used to create the mesh's tris
        var indices = new int[3 * (sides - 2)];
        //Calculate the indices
        int iterator = 0;
        for (int i = 0; i < indices.Length; i += 3)
        {
            indices[i] = 0;
            indices[i + 1] = iterator + 1;
            indices[i + 2] = iterator + 2;
            iterator++;
        }
        //Assign the indices back to the mesh
        mesh.triangles = indices;

        //Create the normals on the vertex level
        var norms = new Vector3[verts.Length];
        //Set all the normals to negative forward
        for (int i = 0; i < verts.Length; i++)
            norms[i] = -Vector3.forward;
        //Assign the normals back to the mesh
        mesh.normals = norms;

        //Create the UVs for the mesh
        var UVs = new Vector2[sides];
        //Set the UVs to be the same as the verts
        for (int i = 0; i < sides; i++)
            UVs[i] = verts[i];
        //Assign the UVs back to the mesh
        mesh.uv = UVs;

        return mesh;
    }
    */
    void OnDestroy()
    {
        if (customMesh != null)
        {
            Destroy(customMesh);
        }
    }
}