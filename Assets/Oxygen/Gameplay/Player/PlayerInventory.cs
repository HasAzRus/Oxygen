using System;
using UnityEngine;

namespace Oxygen
{
    public interface IItem
    {
        string GetName();
        int GetCount();
        int GetPreviousCount();
    }

    [Serializable]
    public class Item : IItem
    {
        [SerializeField] private string _name;
        [SerializeField] private int _count;
        
        public string GetName()
        {
            return _name;
        }

        public int GetCount()
        {
            return _count;
        }

        public int GetPreviousCount()
        {
            return -1;
        }

        public override string ToString()
        {
            return $"Предмет: {_name} в кол-ве - {_count}";
        }
    }
    
    public class Slot : IItem
    {
        private string _name;
        private int _count;

        private int _previousCount;

        private bool _isAssigned;

        protected virtual void OnAssigned(string name)
        {
            
        }

        protected virtual void OnClear()
        {
            
        }

        protected virtual void OnCountChanged(int value)
        {
            
        }

        public void Assign(string name)
        {
            _name = name;
            _isAssigned = true;
            
            OnAssigned(name);
        }

        public void Clear()
        {
            _isAssigned = false;

            OnClear();
        }

        public bool Add(int number)
        {
            if (!_isAssigned)
            {
                return false;
            }

            SetCount(_count + number);

            return true;
        }

        public bool Remove()
        {
            if (!_isAssigned)
            {
                return false;
            }

            SetCount(0);
            
            Clear();

            return true;
        }

        public bool Remove(int number)
        {
            if (!_isAssigned)
            {
                return false;
            }

            if (_count - number > 0)
            {
                SetCount(_count - number);
            }
            else
            {
                SetCount(0);
                
                Clear();
            }

            return true;
        }
        
        public void SetCount(int value)
        {
            _previousCount = _count;
            _count = value;

            OnCountChanged(value);
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCount()
        {
            return _count;
        }

        public int GetPreviousCount()
        {
            return _previousCount;
        }

        public bool CheckAssigned()
        {
            return _isAssigned;
        }
    }
    
    public class PlayerInventory : Behaviour
    {
        public event Action<IItem> Placed;
        public event Action<IItem> Removed;
        
        [SerializeField] private int _capacity;

        private Slot[] _slots;

        protected override void Awake()
        {
            base.Awake();
            
            _slots = new Slot[_capacity];

            for (var i = 0; i < _capacity; i++)
            {
                _slots[i] = new Slot();
            }
        }

        public bool Place(string name, int count)
        {
            foreach (var slot in _slots)
            {
                if (slot.CheckAssigned())
                {
                    if (slot.GetName() != name)
                    {
                        continue;
                    }
                }
                else
                {
                    slot.Assign(name);
                }

                slot.Add(count);
                
                Debug.Log($"Был добавлен предмет {name} в кол-ве {count}");
                
                Placed?.Invoke(slot);

                return true;
            }

            return false;
        }

        public bool Place(IItem item)
        {
            return Place(item.GetName(), item.GetCount());
        }

        public bool Remove(string name)
        {
            foreach (var slot in _slots)
            {
                if (!slot.CheckAssigned())
                {
                    continue;
                }

                if (slot.GetName() == name)
                {
                    continue;
                }
                
                slot.Remove();
                
                Debug.Log($"Предмет был удален: {name}");
                
                Removed?.Invoke(slot);

                return false;
            }

            return false;
        }

        public bool Remove(string name, int count)
        {
            foreach (var slot in _slots)
            {
                if (!slot.CheckAssigned())
                {
                    continue;
                }

                if (slot.GetName() != name)
                {
                    continue;
                }

                slot.Remove(count);
                
                Debug.Log($"Предмет был удален в кол-ве: {name} - {count}");
                
                Removed?.Invoke(slot);

                return true;
            }

            return false;
        }

        public bool Remove(IItem item)
        {
            return Remove(item.GetName(), item.GetCount());
        }

        public bool CheckIsHave(string name)
        {
            foreach (var slot in _slots)
            {
                if (!slot.CheckAssigned())
                {
                    continue;
                }

                if (slot.GetName() != name)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool CheckIsHave(string name, int count)
        {
            foreach (var slot in _slots)
            {
                if (!slot.CheckAssigned())
                {
                    continue;
                }

                if (slot.GetName() != name)
                {
                    continue;
                }

                if (slot.GetCount() < count)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool CheckIsHave(IItem item)
        {
            return CheckIsHave(item.GetName(), item.GetCount());
        }

        public IItem[] GetItems()
        {
            var items = new IItem[_capacity];

            for (var i = 0; i < _capacity; i++)
            {
                var slot = _slots[i];
                
                items[i] = slot.CheckAssigned() ? _slots[i] : null;
            }
            
            return items;
        }
    }
}