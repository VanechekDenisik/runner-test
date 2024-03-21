using Core.Common.Entities;
using UnityEngine;

namespace Levels
{
    //The level generation is rather simple now. So in future it will definitely require more complex logic.
    //That's why even such simple code should be well written. A developer should always keep in mind that
    //human brains (fast memory) fits only 2-6 chunks of information. That's why refactoring patterns 
    //like "Extract method", "Inline method", "Replace Temp with Query" and other should be used very often.
    public class LevelGenerator : EntityComponentWithConfig<LevelGeneratorConfig>
    {
        [SerializeField] private LevelPart startingLevelPart;
        
        private void Awake()
        {
            LevelPart previousPart = startingLevelPart;
            for (var partId = 0; partId < 40; partId++)
            {
                //I am using "Inline Method" refactoring pattern here to simplify amount of chunks of information for a future developer.
                var levelPart = Config.SpawnRandomPart(transform, previousPart?.EndPoint.position ?? Vector3.zero);
                SpawnCollectable(levelPart);
                previousPart = levelPart;
            }
        }

        //I used "Extract method" refactoring pattern here for also reducing amount of chunks of information for understanding.
        //Otherwise the code was requiring comments.
        private void SpawnCollectable(LevelPart levelPart)
        {
            //I use the word "result" here for the same purpose.
            var result = Config.RandomCollectable()?.Spawn(levelPart.transform);
            if (result == null) return;
            result.transform.position = levelPart.CoinSpawnPoint.position;
        }
    }
}
