using Skul.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSet : MonoBehaviour
{
    public Vector2[] potalPos=new Vector2[2];
    public PotalType[] potalType=new PotalType[2];
    public Vector2 rewardPos;

    public Vector2 startPos;

    public Vector2 mapMinBoundary;
    public Vector2 mapMaxBoundary;

    private void Awake()
    { 
        GameManager.instance.OnChangeScene?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawLine(mapMinBoundary,mapMaxBoundary);
    }
}
