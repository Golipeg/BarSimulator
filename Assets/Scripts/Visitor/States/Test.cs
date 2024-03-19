using System;
using UnityEngine;

namespace Visitor
{
    public class Test : MonoBehaviour

    {
        private void OnTriggerStay(Collider other)
        {
            Debug.Log("State");
        }
    }
}