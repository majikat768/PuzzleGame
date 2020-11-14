using UnityEngine;
using UnityEngine.Tilemaps;

public class CellularMapGenerator : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField]
    Tile block;

    int W = 64;
    int H = 32;

    float fillPerc = 0.3f;

    int iterations = 100;

    void Start()
    {
        Camera.main.transform.position = new Vector3(W / 2, H / 2, Camera.main.transform.position.z);
        //Camera.main.orthographicSize = H / 2;
        tilemap = GetComponent<Tilemap>();

        InitMap();

        // comment out to see iterations happen
        // (see also Update() function)
        for(int i = 0; i < iterations; i++)
        {
            tilemap = IterateMap();
        }
        GameObject.FindWithTag("Player").transform.position = SpawnPoint();
    }

    void InitMap()
    {
        for(int i = 0; i < W; i++)
        {
            for(int j = 0; j < H; j++)
            {
                if(i == 0 || i == W-1 || j == 0 || j == H-1)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), block);
                }
                else if(Random.value < fillPerc)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), block);
                }
            }
        }

    }
    Tilemap IterateMap()
    {
        Tilemap newmap = tilemap;
        int birthLim = 2;
        int deathLim = 2;

        for(int i = 1; i < W-1; i++)
        {
            for(int j = 1; j < H-1; j++)
            {
                Vector3Int pos = new Vector3Int(i, j, 0);
                int neighbors = NeighborCount(pos);

                if(tilemap.GetTile(pos) != null)
                {
                    if (neighbors < deathLim)
                        newmap.SetTile(pos, null);
                }
                else
                {
                    if (neighbors > birthLim)
                        newmap.SetTile(pos, block);
                }

            }
        }

        return newmap;
    }

    int NeighborCount(Vector3Int pos)
    {
        int count = 0;
        for (int i = pos.x - 1; i <= pos.x + 1; i++) {
            for (int j = pos.y - 1; j <= pos.y + 1; j++)
            {
                if (i == pos.x || j == pos.y && (i != pos.x || j != pos.y))
                {
                    if (tilemap.GetTile(new Vector3Int(i, j, 0)) != null) count++;
                }
            }
        }

        return count;
    }

    void Update()
    {
        // uncomment to see iterations
        // this may break spawnpoint()
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            IterateMap();
        }
        */
        
    }

    Vector3Int SpawnPoint()
    {
        Vector3Int p = new Vector3Int(Random.Range(1, W - 1), Random.Range(1, H - 1), 0);
        while(tilemap.GetTile(p) != null)
        {
            p = new Vector3Int(Random.Range(1, W - 1), Random.Range(1, H - 1), 0);

        }

        return p;
    }
}
