using Skul.GameElement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSet : MonoBehaviour
{
    private void Awake()
    { 
        GameManager.instance.OnChangeScene?.Invoke(this);
    }

    public Vector2[] potalPos = new Vector2[2];
    public PotalType[] potalType = new PotalType[2];
    public Vector2 rewardPos;

    public Vector2 startPos;

    public Vector2 mapMinBoundary;
    public Vector2 mapMaxBoundary;

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawLine(mapMinBoundary,mapMaxBoundary);
    }
}
