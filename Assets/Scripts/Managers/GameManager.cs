using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Visitor;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private VisitorsController _visitorsController;


        public  void Start()
        {
            _levelManager.Initialize();
            _visitorsController.Initialize();
        }
        private async Task Tests()
        {
            await Task.Delay(1000);
            await Task.Run(() => Debug.Log("123"));
        }

        private async void  Awake()
        {
            await Tests();
        }
    }
}