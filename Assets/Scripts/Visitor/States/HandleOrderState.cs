using System;
using System.Collections.Generic;
using System.Diagnostics;
using DefaultNamespace;
using IngameStateMachine;
using Orders;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace Visitor
{
    public class HandleOrderState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private Order _order;
        private Dictionary<ItemType, int> _receivedOrders = new();
        private bool _isActiveState;


        public void Construct(Order order)
        {
            _order = order;
        }

        private void OnTriggerStay(Collider collider)
        {
            Debug.Log(collider.name);
          
            if (!_isActiveState)
            {
                Debug.Log("isActive");
                return;
            }

            if (collider.TryGetComponent(out BarManager barManager))
            {
                Debug.Log(barManager);
                foreach (var orderItem in _order.Orders)
                {
                    ItemType itemType = orderItem.Key;
                    int quantityNeeded = orderItem.Value;

                    while (quantityNeeded > 0)
                    {
                        if (barManager.TryGetItemFromBar(itemType, out Item item))
                        {
                            item.GiveItemForHolders(transform);
                            // Здесь логика обработки полученного предмета, например, добавление его в список полученных заказов
                            if (!_receivedOrders.ContainsKey(itemType))
                            {
                                _receivedOrders.Add(itemType, 1);
                            }
                            else
                            {
                                _receivedOrders[itemType]++;
                            }

                            quantityNeeded--;
                        }
                        else
                        {
                            break; // Если предмет недоступен, прерываем цикл
                        }
                    }
                }

                if (CheckServedOrder())
                {
                    _stateMachine.Enter<MoveToTableState>();
                }
            }
        }

        private bool CheckServedOrder()
        {
            if (_receivedOrders.Count != _order.Orders.Count)
            {
                return false;
            }

            foreach (var orderItem in _order.Orders)
            {
                if (!_receivedOrders.TryGetValue(orderItem.Key, out int receivedQuantity) ||
                    receivedQuantity != orderItem.Value)
                {
                    return false;
                }
            }

            return true;
        }


        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _isActiveState = true;
            Debug.Log(_isActiveState);
        }

        public void OnExit()
        {
            _isActiveState = false;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}