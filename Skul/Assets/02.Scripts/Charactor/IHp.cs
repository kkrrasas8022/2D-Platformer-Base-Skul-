using System;
using UnityEngine;

namespace Skul.Character
{
    public interface IHp
    {
        float hp { get; set; }
        float hpMax { get; set; }
        float hpMin { get; }

        event Action<float> onHpChanged;
        event Action<float> onHpIncreased;
        event Action<float> onHpDecreased;
        event Action<float> onHpMaxChanged;
        event Action onHpMin;
        event Action onHpMax;

        public void Damage(GameObject damager, float amount);
        public void Heal(GameObject healer, float amount);
    }
}