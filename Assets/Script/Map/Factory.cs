using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    [HideInInspector]
    public float factory_extends_x, factory_extends_z;
    Vector3 center;

    [Header("Enemy")]
    public GameObject enemyPrefabs;
    public int enemy_Count;
    [HideInInspector]
    public List<GameObject> enemyArray;

    [Header("Enemy Size")]
    public float min_Height;
    public float max_Height;
    public float min_Lenght;
    public float max_Length;

    [Space]

    [Header("Coin")]
    public GameObject coinPrefabs;
    public int coin_Count;
    List<GameObject> coinArray;

    bool isBridge = true;
    public bool isAnimasyon;
    
    Vector3 oldTransform;

    private void Start()
    {
        center = this.gameObject.transform.position;
        oldTransform = center;

        enemyArray = new List<GameObject>();
        coinArray = new List<GameObject>();

        Spawn(enemyPrefabs, enemy_Count, enemyArray);
        Spawn(coinPrefabs, coin_Count, coinArray);
    }

    //Player Köprüde
    public void isLevelBridge(bool state)
    {
        isBridge = state;
    }

    private void Update()
    {
        oldTransform = transform.position;
    }

    private void LateUpdate()
    {
        TransformChange();
    }

    bool canTrans = false;

    bool hasChangeZ()
    {
        return oldTransform.z != transform.position.z ? true : false;
    }

    bool hasChangeX()
    {
        return oldTransform.x != transform.position.x ? true : false;
    }

    void TransformChange()
    {
        //factory Yer degistirdiginde
        if (transform.hasChanged)
        {transform.hasChanged = !transform.hasChanged;
            
            //Mola
            if (isBridge)
            {
                canTrans = false;
            }

            if(!isBridge)
            {
                if (hasChangeZ())
                {
                    canTrans = true;
                }
            }

            if (canTrans)
            {
                Transform(enemyArray, factory_extends_x, factory_extends_z, true);
                Transform(coinArray, factory_extends_x, factory_extends_z, false);
            }
        }
    }

    //Nesne , Sayısı , x ekseninde yerlestirilecek siınırları
    void Spawn( GameObject prefabs , float count, List<GameObject> arrayObje)
    {
        for (int i = 0; i < count; i++)
        {
            arrayObje.Add(Instantiate(prefabs, new Vector3( 0,0 , -20 ) , Quaternion.identity));
        }
    }

    void Transform(List<GameObject> arrayObje, float extends_X, float extends_Z , bool isRandomScale)
    {
        center = this.gameObject.transform.position;
        foreach (var item in arrayObje)
        {
            Vector3 local = new Vector3(Random.Range(center.x - extends_X, center.x + extends_X), 3.05f, Random.Range(center.z - extends_Z, center.z + extends_Z));
            item.transform.position = local;
            if (isRandomScale)
            {
                RandomScale(item);
                if (isAnim)
                {
                    StartAnimasyon(item, local);
                }
                else
                {
                    StopAnimasyon(item);
                    item.transform.position = local;
                }
            }
        }
    }

    public bool isAnim;
    public bool animDir;
    void StartAnimasyon(GameObject item , Vector3 pos)
    {
        if (item != null)
        {
            item.GetComponent<EnemyMove>().enabled = false;
            item.GetComponent<EnemyMove>().ChangePos(pos, animDir);
            item.GetComponent<EnemyMove>().enabled = true;
        }
    }
    void StopAnimasyon(GameObject item)
    {
        if (item != null)
        {
            item.GetComponent<EnemyMove>().enabled = false;
        }
    }


    void RandomScale(GameObject obje)
    {
        float randomHeight = Random.Range(min_Height, max_Height);
        float randomLength = Random.Range(min_Lenght , max_Length);
        obje.transform.localScale = new Vector3(randomLength, randomHeight, 1);
    }
}
