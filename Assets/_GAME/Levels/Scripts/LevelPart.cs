using System;
using UnityEngine;

namespace Levels
{
    public class LevelPart : MonoBehaviour
    { 
        [field: SerializeField] public Transform EndPoint { get; private set; }
        [field: SerializeField] public Transform CoinSpawnPoint { get; private set; }
        
        private void OnDrawGizmos()
        {
            DrawEndPoint();
            DrawCoinSpawnPoint();
        }

        private void DrawEndPoint()
        {
            if (EndPoint == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(EndPoint.position, 0.3f);
        }
        
        private void DrawCoinSpawnPoint()
        {
            if (CoinSpawnPoint == null) return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(CoinSpawnPoint.position, 0.3f * Vector3.one);
        }
    }
}