using System;
using System.Collections;
using System.Collections.Generic;
using Skul.Data;
using Skul.Item;
using Skul.Tools;
using UnityEngine;

namespace Skul.GameManager
{
    public enum MapReward
    {
        Coin,
        Weapon,
        Bone
    }
    public class GameManager : SingletonMonoBase<GameManager>
    {
        
        public List<ItemBox> weaponBox;
        public List<ItemBox> graves;
        public List<Potal> potals;

        public ItemRate itemRate;
        public int percentageType;

        public int Rarepercentage;
        public int Uniquepercentage;
        public int Legendpercentage;

        public bool isReward;

        public MapReward mapReward;

        public bool isClear;

        public int mapEnemyCount;

        public Vector3 mapBoxPosition;
        public Vector2 mapSize;

        protected override void Awake()
        {
            base.Awake();
        }

        public void GoNextMap()
        {
            mapEnemyCount = 0;
            mapSize = Vector3.zero;
            isClear= true;
            isReward= false;
            percentageType=UnityEngine.Random.Range(0, 100);
        }
        public void Update()
        {
            if (mapEnemyCount == 0)
                isClear = true;
            if(isClear)
            {
                switch (percentageType)
                {
                    case int i when i > Legendpercentage:
                        itemRate = ItemRate.Legend;
                        break;
                    case int i when i >Uniquepercentage:
                        itemRate = ItemRate.Unique;
                        break;
                    case int i when i > Rarepercentage:
                        itemRate = ItemRate.Rare;
                        break;
                    default:
                        itemRate = ItemRate.Normal;
                        break;
                }
                if(isReward==false)
                {
                    switch (mapReward)
                    {
                        case MapReward.Coin:
                            break;
                        case MapReward.Weapon:
                            {
                                Instantiate(weaponBox[(int)itemRate],mapBoxPosition,Quaternion.identity);
                            }
                            break;
                        case MapReward.Bone:
                            Instantiate(graves[(int)itemRate], mapBoxPosition, Quaternion.identity);
                            break;
                    }
                    isReward = true ;
                }
            }
        }
    }
}