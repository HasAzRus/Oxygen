using System;
using UnityEngine;

namespace Oxygen
{
    [Serializable]
    public class SlotDrawer : Slot
    {
        //...
    }
    
    public class InventoryUserInterface : UserInterfacePage
    {
        [SerializeField] private SlotDrawer[] _drawerSlots;

        public void Remove(IItem item)
        {
            foreach (var drawerSlot in _drawerSlots)
            {
                if (!drawerSlot.CheckAssigned())
                {
                    continue;
                }

                if (drawerSlot.GetName() != item.GetName())
                {
                    continue;
                }

                if (item.GetPreviousCount() > 0)
                {
                    var difference = item.GetPreviousCount() - item.GetCount();
                    drawerSlot.Remove(difference);
                }
                else
                {
                    drawerSlot.Remove();
                }

                return;
            }
        }

        public void Place(IItem item)
        {
            foreach (var drawerSlot in _drawerSlots)
            {
                if (drawerSlot.CheckAssigned())
                {
                    if (drawerSlot.GetName() != item.GetName())
                    {
                        continue;
                    }
                }
                else
                {
                    drawerSlot.Assign(item.GetName());
                }

                drawerSlot.SetCount(item.GetCount());

                return;
            }
        }
    }
}