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
        None,
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
        private Dictionary<PotalType,Potal> potalsDic;

        public Potal[] nowMapPotal=new Potal[2];

        public ItemRate itemRate;
        public int percentageType;

        public int Rarepercentage;
        public int Uniquepercentage;
        public int Legendpercentage;

        public bool isReward;

        public MapReward mapReward;

        public bool isClear;

        public Action StageClear;
        public Action EnemyDie;

        public int mapEnemyCount;

        public Vector3 mapBoxPosition;
        public Vector2 mapSize;
        public Vector2 mapCenter;

        public Action<SceneSet> OnChangeScene;

        protected override void Awake()
        {
            base.Awake();
            potalsDic = new Dictionary<PotalType, Potal>();
            foreach(Potal item in potals)
            {
                potalsDic.Add(item.type, item);
            }
            _player=GameObject.FindWithTag("Player").GetComponent<Player>();
            
            MainUI.instance._player = this._player;
            MainUI.instance.Show();
            //_inventoryUI = _player.inventoryUI;
            DontDestroyOnLoad(_player);
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
                if (isReward == false)
                {
                    switch (mapReward)
                    {
                        case MapReward.None:
                            break;
                        case MapReward.Coin:
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
                    isReward = true;
                }
            };
            OnChangeScene += (sceneSet) =>
            {
                mapEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                mapSize = sceneSet.mapSize;
                isClear = false;
                isReward = false;
                percentageType = UnityEngine.Random.Range(0, 100);

                int r=0;
                for(int i=0;i<nowMapPotal.Length;i++)
                {
                    int randomtype=UnityEngine.Random.Range(0,4);
                    if (i > 0)
                    {
                        if(randomtype==r)
                        {
                            i--;
                            continue;
                        }
                    }
                    r = randomtype;
                    nowMapPotal[i] = Instantiate(potalsDic[(PotalType)r], sceneSet.potalPos[i], Quaternion.identity).GetComponent<Potal>();
                    
                }
               // nowMapPotal[0] = Instantiate(potalsDic[sceneSet.potalType[0]], sceneSet.potalPos[0], Quaternion.identity).GetComponent<Potal>();
                //nowMapPotal[1] = Instantiate(potalsDic[sceneSet.potalType[1]], sceneSet.potalPos[1], Quaternion.identity).GetComponent<Potal>();

                _player.transform.position = sceneSet.startPos;
                mapBoxPosition = sceneSet.rewardPos;
                mapCenter = sceneSet.mapCenter;
              
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
            if (mapEnemyCount == 0)
                isClear = true;
            if(isClear)
            {
                
            }
        }
    }
}