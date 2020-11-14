using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField]
    Tile wall;
    [SerializeField]
    Tile corner;
    Dictionary<char, Tile> walls;
    BSP bsp;
    List<Room> rooms;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bsp = new BSP();
        rooms = bsp.GetRooms();
        foreach(Room r in rooms)
        {
            for(int i = r.x+1; i < r.x + r.width; i++)
            {
                Vector3Int sTilePos = new Vector3Int(i, r.y, 0);
                Vector3Int nTilePos = new Vector3Int(i, r.y+r.height, 0);
                if (
                    !r.doors.Contains(sTilePos) 
                    )
                {
                    tilemap.SetTile(sTilePos, wall);
                    RotateTile(sTilePos, "S");
                }
                if (
                    !r.doors.Contains(nTilePos) 
                    )
                {
                    tilemap.SetTile(nTilePos, wall);
                    RotateTile(nTilePos, "N");
                }
            }
            for(int i = r.y+1; i < r.y + r.height; i++)
            {
                Vector3Int eTilePos = new Vector3Int(r.x+r.width, i, 0);
                Vector3Int wTilePos = new Vector3Int(r.x, i, 0);
                if (
                    !r.doors.Contains(eTilePos)
                    )
                {
                    tilemap.SetTile(eTilePos, wall);
                    RotateTile(eTilePos, "E");
                }
                if (!r.doors.Contains(wTilePos))
                {
                    tilemap.SetTile(wTilePos, wall);
                    RotateTile(wTilePos, "W");
                }

            }
            Vector3Int nwTilePos = new Vector3Int(r.x, r.y + r.height, 0);
            Vector3Int swTilePos = new Vector3Int(r.x, r.y, 0);
            Vector3Int neTilePos = new Vector3Int(r.x + r.width, r.y + r.height, 0);
            Vector3Int seTilePos = new Vector3Int(r.x + r.width, r.y, 0);
            if (!r.doors.Contains(nwTilePos)) {
                tilemap.SetTile(nwTilePos, corner);
                RotateTile(nwTilePos, "NW");
            }
            if (!r.doors.Contains(neTilePos))
            {
                tilemap.SetTile(neTilePos, corner);
                RotateTile(neTilePos, "NE");
            }
            if (!r.doors.Contains(swTilePos))
            {
                tilemap.SetTile(swTilePos, corner);
                RotateTile(swTilePos, "SW");
            }
            if (!r.doors.Contains(seTilePos))
            {
                tilemap.SetTile(seTilePos, corner);
                RotateTile(seTilePos, "SE");
            }
        }
    }

    void RotateTile(Vector3Int pos, string dir)
    {
        switch(dir)
        {
            case "N":
                tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 90), Vector3.one));
                break;
            case "E":
                break;
            case "S":
                tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -90), Vector3.one));
                break;
            case "W":
                tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 180), Vector3.one));
                break;
            case "NW":
                break;
            case "NE":
                tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -90), Vector3.one));
                break;
            case "SW":
                tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 90), Vector3.one));
                break;
            case "SE":
                tilemap.SetTransformMatrix(pos, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 180), Vector3.one));
                break;
            default:
                break;
        }
    }
    void Update()
    {
    }
}
