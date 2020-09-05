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
    
    public GameObject border;
    public GameObject player;

    private Vector3 mousePosition;
    

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && !isRuning)
        {
            CreateItem();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !isRuning)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        border.SetActive(true);
        isRuning = true;
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
