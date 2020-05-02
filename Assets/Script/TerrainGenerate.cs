using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainGenerate : MonoBehaviour {
    public int x = 0;
    //private NoiseModule m_mountainNoise = new PerlinNoise(1);
    //private NoiseModule m_mountainNoiseRidged = new RidgedNoise(1);
    Terrain terrain;
    void generateTerrain2()
    {
        NoiseModule m_mountainNoise = new PerlinNoise(1);
        NoiseModule m_mountainNoiseRidged = new RidgedNoise(1);
        terrain = this.GetComponent<Terrain>();

        int heightmapResolution = terrain.terrainData.heightmapResolution;
        float ratio = (float)terrain.terrainData.size.x / (float)heightmapResolution;
        float[,] terrainHeight = new float[heightmapResolution, heightmapResolution];
        //float hx = Mathf.Clamp((y1 - y0) / (z1 - z0) * (worldPosZ - z0) + y1, -4, 8);
        for (int z = 0; z < heightmapResolution; z++)
        {
            float worldPosZ = z * ratio;// (z + z / 171 * (heightmapResolution - 1)) * ratio;
            //float hx = Random.Range(-4.0f, 8.0f);
            float hx = Mathf.Clamp((float)1 / 171 * z * ratio + 1, -4, 8);
            for (int x = 0; x < heightmapResolution; x++)
            {
                float worldPosX = x * ratio;//(x + x / 171 * (heightmapResolution - 1)) * ratio;
                float mountainsPerlin = m_mountainNoise.FractalNoise2D(worldPosX, worldPosZ, 4, 1000, 0.4f);
                float mountainsRidged = m_mountainNoiseRidged.FractalNoise2D(worldPosX, worldPosZ, 4, 1000, 0.2f);
                float height = (mountainsRidged + mountainsPerlin) + 0.03f * hx;
                terrainHeight[z, x] = height;
            }
        }
        terrain.terrainData.SetHeights(0, 0, terrainHeight);
    }
    private void OnValidat()
    {
       // generateTerrain2();
    }
    void Start()
    {
        generateTerrain2();
        //generateTerrain();
    }

}
