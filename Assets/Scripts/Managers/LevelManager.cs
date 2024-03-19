using Furniture;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LoungeManager _loungeManager;

        public void Initialize()
        {
            _loungeManager.Initialize();
        }
    }
}