using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public bool isRuning = false;
    public static int curLevel = 0;
    public bool isWin = false;
    public bool canCreate = true;

    public string playerPath = "Prefabs/Player";
    public string goalPath = "Prefabs/Goal";
    
    public static bool isHave = false;
    
    public GameObject border;

    private Vector3 mousePosition;

    public Vector2 playerPos;
    

    [Serializable]
    public class Item
    {
        public int level;
        public GameObject[] moreTimesItem;
    }

    public Item[] items;
    public List<GameObject> oneTimesItems;

    public static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isHave)
        {
            isHave = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && !isRuning && canCreate)
        {
            CreateItem();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !isRuning && oneTimesItems.Count == 0)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        playerPos = Player._instance.transform.position;
        Instantiate(border, Vector3.zero, Quaternion.identity);
        isRuning = true;
        Player._instance.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void CreateItem()
    {
        Vector3 targerMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        int choose = Random.Range(0, 2);
        if (choose == 1 || oneTimesItems.Count == 0)
        {
            int index = Random.Range(0, items[curLevel].moreTimesItem.Length);
            Instantiate(items[curLevel].moreTimesItem[index], targerMousePos, Quaternion.identity);
        }
        else
        {
            int index = Random.Range(0, oneTimesItems.Count);
            Instantiate(oneTimesItems[index], targerMousePos, Quaternion.identity);
            oneTimesItems.RemoveAt(index);
        }
    }
}
