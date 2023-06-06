using System;
using UnityEngine;

namespace Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        public bool canSpawnPowerUp;
        public bool canSpawnMonster;
    
        public void SpawnPowerUp()
        {
        
        }

        public void SpawnMonster()
        {
       
        }

        public void Reset()
        {
           
        }
    }
}

