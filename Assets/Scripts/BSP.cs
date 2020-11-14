using UnityEngine;
using System.Collections.Generic;

// Binary Space Partioning procedural map generator
//

public class BSP
{
    int W = 48;
    int H = 24;
    public Vector2Int GetSize() { return new Vector2Int(this.W, this.H); }
    List<Room> Rooms;
    public List<Room> GetRooms() { return this.Rooms; }
    int MinRoomArea = 64;
    int MinRoomDim = 5;

    public BSP()
    {
        Rooms = new List<Room>();
        Room r = new Room(0, 0, W, H);
        Rooms.Add(r);
        BuildRoom(r, 0);
        //BufferRooms();
    }

    // this function adds gaps between rooms
    // for aesthetic purposes or something
    // doesn't work
    void BufferRooms()
    {
        List<Room> newRooms = new List<Room>();
        foreach (Room r in Rooms)
        {
            Room r2 = r;
            r2.x += Random.Range(r2.width / 4, r2.width / 2);
            r2.width -= Random.Range(r2.width / 4, r2.width / 2);
            r2.y += Random.Range(r2.height / 4, r2.height / 2);
            r2.height -= Random.Range(r2.height / 4, r2.height / 2);
            
            for(int i = 0; i < r2.doors.Count; i++) 
            {
                Vector3Int door = r2.doors[i];
                if (door.x == r.x)
                    door.x = r2.x;
                else if (door.x == r.x + r.width)
                    door.x = r2.x + r2.width;
                else if (door.y == r.y)
                    door.y = r2.y;
                else if (door.y == r.y + r.height)
                    door.y = r2.y + r2.height;
            }
            newRooms.Add(r2);
        }
        this.Rooms.Clear();
        this.Rooms.AddRange(newRooms);
    }

    //recursive function.
    // splits current room into two, then recurses.
    // if current room is too small, return
    void BuildRoom(Room r, int depth)
    {
        if ((r.width) * (r.height) <= MinRoomArea) return;
        //Debug.Log(r.width*r.height);
        Room r1, r2;
        if (r.width > r.height)
        {
            int buffer = r.width / 2 - MinRoomDim;
            if (buffer < 0) return;
            int rWidth = r.width / 2 + Random.Range(-buffer, buffer);
            int iter = 0;
            Rect r1Rect = new Rect(r.x, r.y, rWidth - 1, r.height);
            Rect r2Rect = new Rect(r.x + rWidth, r.y, r.width - rWidth, r.height);

            r1 = new Room(r1Rect.x, r1Rect.y, r1Rect.width, r1Rect.height);
            r2 = new Room(r.x + rWidth, r.y, r.width - rWidth, r.height);
            int d = r.y + r.height / 2 + Random.Range(-r.height / 4, r.height / 4);
            r1.doors.Add(new Vector3Int(r.x + rWidth - 1, d, 0));
            r2.doors.Add(new Vector3Int(r.x + rWidth, d, 0));
        }
        else
        {
            int buffer = r.height / 2 - MinRoomDim;
            if (buffer < 0) return;
            int rHeight = r.height / 2 + Random.Range(-buffer, buffer);
            int iter = 0;
            r1 = new Room(r.x, r.y, r.width, rHeight - 1);
            r2 = new Room(r.x, r.y + rHeight, r.width, r.height - rHeight);

            int d = r.x + r.width / 2 + Random.Range(-r.width / 4, r.width / 4);
            r1.doors.Add(new Vector3Int(d, r.y + rHeight - 1, 0));
            r2.doors.Add(new Vector3Int(d, r.y + rHeight, 0));
        }
        r1.doors.AddRange(r.doors);
        r2.doors.AddRange(r.doors);
        Rooms.Remove(r);
        Rooms.Add(r1);
        Rooms.Add(r2);
        BuildRoom(r1, depth + 1);
        BuildRoom(r2, depth + 1);
    }

    bool DoorHere(Room r, int val, char dim)
    {
        foreach (Vector3Int d in r.doors)
        {
            if (dim == 'x' || dim == 'X')
            {
                if(Mathf.Abs(d.x - val) <= 2)
                    return true;
            }
            else if(dim == 'y' || dim == 'Y')
            {
                if(Mathf.Abs(d.y - val) <= 2)
                    return true;
            }
        }
        return false;
    }


}

public class Room
{
    public int x, y, width, height;
    public List<Vector3Int> doors;

    public Room(int x, int y, int width, int height)
    {
        doors = new List<Vector3Int>();
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public Room(float x, float y, float width, float height)
    {
        doors = new List<Vector3Int>();
        this.x = (int)x;
        this.y = (int)y;
        this.width = (int)width;
        this.height = (int)height;
    }


}
