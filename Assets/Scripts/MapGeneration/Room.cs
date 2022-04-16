using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public abstract class Room
    {
        protected GameObject _ground;
        protected GameObject _wall;
        protected ERoomSize _roomSize;
        protected int _width;
        protected int _height;
        protected System.Random _random = new System.Random((int)(System.DateTime.Now.Ticks));
        protected abstract void AssignRoomSize(ERoomSize roomSize);
        protected abstract void InitRoom();
    }
}
