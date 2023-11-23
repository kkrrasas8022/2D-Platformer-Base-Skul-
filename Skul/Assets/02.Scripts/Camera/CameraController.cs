using Skul.Character.PC;
using Skul.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float smoothing = 0.2f;
    [SerializeField] private Vector2 minCameraBoundary;
    [SerializeField] private Vector2 maxCameraBoundary;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        minCameraBoundary = GameManager.instance.mapMinBoundary + new Vector2(9, 2); ;
        maxCameraBoundary = GameManager.instance.mapMaxBoundary-new Vector2(9,2);
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_player.transform.position.x,_player.transform.position.y,this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
