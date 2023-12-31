using System;
using System.Collections;
using System.Collections.Generic;
using Skul.Character.PC;
using Skul.Data;
using Skul.Item;
using Skul.Tools;
using Skul.UI;
using UnityEngine;


namespace Skul.GameElement
{
    public enum MapReward
    {
        None,
        Coin,
        Weapon,
        Bone
    }
    public class GameManager : SingletonMonoBase<GameManager>
    {
        [SerializeField] public Player player;
        [SerializeField] public int startCoin;

        [SerializeField] public CoinMount coin;


        public List<ItemBox> weaponBox;
        public List<ItemBox> graves;
        public List<Potal> potals;
        private Dictionary<PotalType, Potal> potalsDic;

        public Potal[] nowMapPotal = new Potal[2];

        public ItemRate itemRate;
        public int percentageType;

        public int Rarepercentage;
        public int Uniquepercentage;
        public int Legendpercentage;



        public MapReward mapReward;



        public Action StageClear;
        public Action EnemyDie;

        public int mapEnemyCount;

        public Vector3 mapBoxPosition;
        public Vector2 mapMinBoundary;
        public Vector2 mapMaxBoundary;


        public Action<SceneSet> OnChangeScene;

        protected override void Awake()
        {
            base.Awake();
            potalsDic = new Dictionary<PotalType, Potal>();
            foreach (Potal item in potals)
            {
                potalsDic.Add(item.type, item);
            }
            player = GameObject.FindWithTag("Player").GetComponent<Player>();

            
            MainUI.instance.Show();
            //_inventoryUI = _player.inventoryUI;
            DontDestroyOnLoad(player);
            //DontDestroyOnLoad(_inventoryUI);
            Debug.Log("GameManager");
            EnemyDie += () =>
            {
                mapEnemyCount--;
                if (mapEnemyCount == 0)
                    StageClear?.Invoke();
            };

            StageClear = () =>
            {
                switch (percentageType)
                {
                    case int i when i > Legendpercentage:
                        itemRate = ItemRate.Legend;
                        break;
                    case int i when i > Uniquepercentage:
                        itemRate = ItemRate.Unique;
                        break;
                    case int i when i > Rarepercentage:
                        itemRate = ItemRate.Rare;
                        break;
                    default:
                        itemRate = ItemRate.Normal;
                        break;
                }

                switch (mapReward)
                {
                    case MapReward.None:
                        break;
                    case MapReward.Coin:
                        {
                            Instantiate(coin, mapBoxPosition, Quaternion.identity);
                        }
                        break;
                    case MapReward.Weapon:
                        {
                            Instantiate(weaponBox[(int)itemRate], mapBoxPosition, Quaternion.identity);
                        }
                        break;
                    case MapReward.Bone:
                        Instantiate(graves[(int)itemRate], mapBoxPosition, Quaternion.identity);
                        break;
                }


            };
            OnChangeScene += (sceneSet) =>
            {
                mapEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                mapMinBoundary = sceneSet.mapMinBoundary;
                mapMaxBoundary = sceneSet.mapMaxBoundary;

                percentageType = UnityEngine.Random.Range(0, 100);

                int r = 0;
                for (int i = 0; i < nowMapPotal.Length; i++)
                {
                    int randomtype = UnityEngine.Random.Range(0, 4);
                    if (i > 0)
                    {
                        if (randomtype == r)
                        {
                            i--;
                            continue;
                        }
                    }
                    r = randomtype;
                    nowMapPotal[i] = Instantiate(potalsDic[(PotalType)r], sceneSet.potalPos[i], Quaternion.identity).GetComponent<Potal>();

                }
                

                player.transform.position = sceneSet.startPos;
                mapBoxPosition = sceneSet.rewardPos;


            };

        }

        public void GoNextMap()
        {
            StartCoroutine(StartLoadNextScene());
        }

        // 로딩씬에 추가된 오브젝트에서 실행되어야 함.
        private IEnumerator StartLoadNextScene()
        {
            AsyncOperation nextScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);

            while (!nextScene.isDone)
            {
                yield return null;
            }
        }

        public void Update()
        {
            if (player.transform.position.x > mapMaxBoundary.x)
                player.transform.position = new Vector3(mapMaxBoundary.x, player.transform.position.y);
            if (player.transform.position.x < mapMinBoundary.x)
                player.transform.position = new Vector3(mapMinBoundary.x, player.transform.position.y);
        }
    }
}