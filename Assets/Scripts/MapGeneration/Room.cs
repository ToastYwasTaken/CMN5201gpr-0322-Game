using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public abstract class Room
    {
        protected GameObject _ground;
        protected GameObject _wall;
        protected int _height;
        protected int _width;
    }
}
