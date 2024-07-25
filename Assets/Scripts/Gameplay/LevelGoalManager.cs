using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class LevelGoalManager : Singleton<LevelGoalManager>
    {
        [SerializeField] private int _goal;

        public int Goal => _goal;
    }
}