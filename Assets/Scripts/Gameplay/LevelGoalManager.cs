using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class LevelGoalManager : Singleton<LevelGoalManager>
    {
        [SerializeField] private int _goal;
        [SerializeField] private int _minValue = 60;

        public int Goal => _goal;
        public int MinValue => _minValue;
    }
}