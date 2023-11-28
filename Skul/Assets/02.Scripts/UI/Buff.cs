
using Skul.Data;
using Skul.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    [SerializeField] private float _times;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _fill;

    [SerializeField] public BuffData data;
    public void SetUp(BuffData data)
    {
         this.data = data;
         _icon.sprite= data.icon;
         _times = data.MaxTime;
    }

    private void Start()
    {
        MainUI.instance.StartBuff(this);
    }

    private void Update()
    {
        _fill.fillAmount = _times / data.MaxTime;
        _times -= Time.deltaTime;
        if(_times<0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        MainUI.instance.EndBuff(this);
    }
}
