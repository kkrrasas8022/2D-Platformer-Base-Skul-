using System;
using System.Collections;
using System.Collections.Generic;
using Skul.Character.PC;
using Skul.Data;
using Skul.Item;
using Skul.Tools;
using Skul.UI;
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
        [SerializeField] private Player _player;
        [SerializeField] public int startCoin;

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
            _player=GameObject.FindWithTag("Player").GetComponent<Player>();
            //_inventoryUI = _player.inventoryUI;
            DontDestroyOnLoad(_player);
            //DontDestroyOnLoad(_inventoryUI);
        }

        public void GoNextMap()
        {
            mapEnemyCount = 0;
            mapSize = Vector3.zero;
            isClear = true;
            isReward = false;
            percentageType = UnityEngine.Random.Range(0, 100);

            // 동기로드
            // 1번씩, 2번씩
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
        }
      //     // 비동기 로드
      //     // 1번씩, 2번씩, 로드씬
      //
      //
      //     StartCoroutine(StartLoadNextScene());
      // }
      //
      // // 로딩씬에 추가된 오브젝트에서 실행되어야 함.
      // private IEnumerator StartLoadNextScene()
      // {
      //     AsyncOperation ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("2번씬");
      //
      //     // 다음 씬 준비 시 바로 로드시키도록 허용하지 않음
      //     ao.allowSceneActivation = false;
      //
      //
      //     // allowSceneActivation 가 false 일 경우
      //     // 0 ~ 0.9
      //     // allowSceneActivation 가 true 일 경우
      //     // 0 ~ 1.0
      //     //ao.progress; // <- 로드 진행 상태
      //
      //     // 로드가 진행중이라면
      //     while (ao.progress < 0.9f)
      //     {
      //         yield return null;
      //
      //
      //         // 로드 진행중 해야 할 일
      //         // ...
      //     }
      //
      //
      //     // 해야될 일 끝남 체크?
      //
      //
      //     // 다음 씬 로드 허용
      //     ao.allowSceneActivation = true;
      // }
      //
        public void Update()
        {
            mapEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
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