using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Companions
{
    public class JoeSamson : Companion
    {
        public float defense = 200;

        public override Info getInfo()
        {
            return new Info();
        }
    }
}