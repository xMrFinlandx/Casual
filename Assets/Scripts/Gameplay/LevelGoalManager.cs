using System.Linq;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class LevelGoalManager : Singleton<LevelGoalManager>
    {
        [SerializeField] private int _goal;
        [SerializeField] private int _minValue;

        public int Goal => _goal;
        public int MinValue => _minValue;

        [ContextMenu("Find Goal and Min Value")]
        private void FindMinMax()
        {
            var doors = FindObjectsByType<FinishDoor>(FindObjectsSortMode.None);

            _goal = doors.Max(item => item.TargetCoinsAmount);
            _minValue = doors.Min(item => item.TargetCoinsAmount);
        }
    }
}