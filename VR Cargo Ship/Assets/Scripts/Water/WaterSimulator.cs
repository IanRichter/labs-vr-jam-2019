﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSimulator : MonoBehaviour
{
	public float squareSize = 0.1f;
	public float wave1Size = 0.2f;
	public float wave2Size = 0.1f;
	public float wave1Height = 0.05f;
	public float wave2Height = 0.1f;

	private MeshFilter meshFilter;
	private Mesh mesh;

	private Rect area;
	private Vector3[] vertices;
	private Vector2[] uvs;
	private int[] indices;
	private float[] heights;

	private Vector2Int size;

	private float currentTime;

	// Start is called before the first frame update
	void Start()
	{
		meshFilter = gameObject.GetComponent<MeshFilter>();
		mesh = meshFilter.sharedMesh;

		area.xMin = 0.0f;
		area.yMin = 0.0f;
		area.xMax = 0.0f; 
		area.yMax = 0.0f;
		for (int i = 0; i < mesh.vertices.Length; i++)
		{
			Vector3 v = mesh.vertices[i];
			if (area.yMin > v.x) area.xMin = v.x;
			if (area.yMax < v.x) area.xMax = v.x;
			if (area.yMin > v.z) area.yMin = v.z;
			if (area.yMax < v.z) area.yMax = v.z;
		}

		mesh = new Mesh();
		meshFilter.mesh = mesh;

		size.x = (int)(area.width / squareSize);
		size.y = (int)(area.height / squareSize);
		
		vertices = new Vector3[size.x * size.y * 6];
		uvs = new Vector2[size.x * size.y * 6];
		indices = new int[size.x * size.y * 6];
		heights = new float[(size.x + 1) * (size.y + 1)];

		for (int i = 0; i < size.x * size.y * 6; i++) indices[i] = i;

		UpdateVertices();

		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
	}

    // Update is called once per frame
    void Update()
	{
		currentTime += Time.deltaTime;

		UpdateVertices();

		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
	}

	void UpdateVertices()
	{
		for (int y = 0; y <= size.y; y++)
		{
			for (int x = 0; x <= size.x; x++)
			{
				heights[x + y * (size.x + 1)] = Mathf.PerlinNoise((x + currentTime) * wave1Size, y * wave1Size) * wave1Height + Mathf.PerlinNoise(x * wave2Size, (y + currentTime) * wave2Size) * wave2Height;
			}
		}

		Vector2 invSize = new Vector2(1.0f / size.x, 1.0f / size.y);

		for (int y = 0; y < size.y; y++)
		{
			for (int x = 0; x < size.x; x++)
			{
				int startVertex = (x + y * size.x) * 6;
				vertices[startVertex + 0] = new Vector3(x * squareSize + area.x, heights[x + y * (size.x + 1)], y * squareSize + area.y);
				vertices[startVertex + 1] = new Vector3(x * squareSize + area.x, heights[x + (y + 1) * (size.x + 1)], (y + 1) * squareSize + area.y);
				vertices[startVertex + 2] = new Vector3((x + 1) * squareSize + area.x, heights[(x + 1) + y * (size.x + 1)], y * squareSize + area.y);

				vertices[startVertex + 3] = new Vector3((x + 1) * squareSize + area.x, heights[(x + 1) + y * (size.x + 1)], y * squareSize + area.y);
				vertices[startVertex + 4] = new Vector3(x * squareSize + area.x, heights[x + (y + 1) * (size.x + 1)], (y + 1) * squareSize + area.y);
				vertices[startVertex + 5] = new Vector3((x + 1) * squareSize + area.x, heights[(x + 1) + (y + 1) * (size.x + 1)], (y + 1) * squareSize + area.y);

				uvs[startVertex + 0] = new Vector2(x * invSize.x, y * invSize.y);
				uvs[startVertex + 1] = new Vector2(x * invSize.x, (y + 1) * invSize.y);
				uvs[startVertex + 2] = new Vector2((x + 1) * invSize.x, y * invSize.y);

				uvs[startVertex + 3] = new Vector2((x + 1) * invSize.x, y * invSize.y);
				uvs[startVertex + 4] = new Vector2(x * invSize.x, (y + 1) * invSize.y);
				uvs[startVertex + 5] = new Vector2((x + 1) * invSize.x, (y + 1) * invSize.y);
			}
		}
	}
}