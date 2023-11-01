using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Skul.Data
{
    public abstract class SkulData : ScriptableObject
    {
        public int id;
        public GameObject Renderer;
        public Sprite SkulFace;
    }
}