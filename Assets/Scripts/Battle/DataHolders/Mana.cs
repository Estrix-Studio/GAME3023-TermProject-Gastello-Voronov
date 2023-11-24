﻿using System;

namespace DataHolders
{
    public interface IReadOnlyMana
    {
        float Current { get; }
        float Max { get; }
        public event Action OnManaChanged;
    
        bool CanSpend(float amount);
    }


    /// <summary>
    /// Manages mana of character.
    /// RegenTick should be called from outside.
    /// Can be extended with future requirements.
    /// </summary>
    public class Mana : IReadOnlyMana
    {
        public float Current => _current;
        public float Max => _max;
    
        private float _current;
        private float _max;
        private float _regenRate;
    
        public event Action OnManaChanged;
    
        //Starts at max mana
        public Mana(float max, float regenRate)
        {
            _max = max;
            _current = max;
            _regenRate = regenRate;
        }

        public void RegenTick()
        {
            _current += _regenRate;
            if (_current > _max)
            {
                _current = _max;
            }
            OnManaChanged?.Invoke();
        }

        public bool CanSpend(float amount)
        {
            return _current >= amount;
        }
    
        public void Spend(float amount)
        {
            if (!CanSpend(amount)) throw new Exception("Cannot spend more mana than character have");
        
            _current -= amount;
            OnManaChanged?.Invoke();
        }
    }
}