using System;
using UnityEngine;

namespace Skul.Character
{
    public interface IMp
    {
        float mp { get; set; }
        float mpMax { get; }
        float mpMin { get; }

        event Action<float> onMpChanged;
        event Action<float> onMpIncreased;
        event Action<float> onMpDecreased;
        event Action<float> onMpMaxIncreased;
        event Action<float> onMpMaxDecreased;
        event Action onMpMin;
        event Action onMpMax;

        public void useMp(float amout);
    }
}