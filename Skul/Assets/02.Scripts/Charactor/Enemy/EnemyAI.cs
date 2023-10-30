using System;
using UnityEngine;

public class EnemyAI:MonoBehaviour
{
    enum AIstep
    {
        None,
        Think,
        TakeARest,
        MoveLeft,
        MoveRight,
        Chase,
        Attack,
    }

}