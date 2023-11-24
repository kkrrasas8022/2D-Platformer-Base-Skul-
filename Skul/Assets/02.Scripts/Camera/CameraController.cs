using Skul.Character.PC;
using Skul.GameElement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float smoothing = 0.2f;
    [SerializeField] private Vector2 minCameraBoundary;
    [SerializeField] private Vector2 maxCameraBoundary;
    [SerializeField] private float _size_y;
    [SerializeField] private float _size_x;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _size_y = GetComponent<Camera>().orthographicSize;
        _size_x = GetComponent<Camera>().orthographicSize* Screen.width / Screen.height;
        minCameraBoundary = GameManager.instance.mapMinBoundary + new Vector2(_size_x,_size_y); 
        maxCameraBoundary = GameManager.instance.mapMaxBoundary - new Vector2(_size_x,_size_y);
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_player.transform.position.x,_player.transform.position.y,this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
