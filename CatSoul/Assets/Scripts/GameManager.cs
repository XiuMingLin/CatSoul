using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LitJson;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    public Transform clones;
    

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
            Instantiate(gameObject);
            clones = GameObject.Find("Clone").transform;
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
        if (SceneManager.GetActiveScene().name == "Welcome")
        {
            isHave = false;
            Destroy(gameObject);
        }
        
        mousePosition = Input.mousePosition;

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
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
            GameObject go = Instantiate(items[curLevel].moreTimesItem[index], targerMousePos, Quaternion.identity);
            go.transform.parent = clones;
        }
        else
        {
            int index = Random.Range(0, oneTimesItems.Count);
            GameObject go = Instantiate(oneTimesItems[index], targerMousePos, Quaternion.identity);
            go.transform.parent = clones;
            oneTimesItems.RemoveAt(index);
        }
    }


    public void SaveAllItem()
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.WriteArrayStart();
        for (int i = 0; i < clones.childCount; i++)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("ItemName");
            writer.Write(clones.GetChild(i).name);
            writer.WritePropertyName("x");
            writer.Write(clones.GetChild(i).position.x);
            writer.WritePropertyName("y");
            writer.Write(clones.GetChild(i).position.y);
            writer.WritePropertyName("z");
            writer.Write(clones.GetChild(i).position.z);
            writer.WriteObjectEnd();
        }
        writer.WriteArrayEnd();
        JsonData jd = JsonMapper.ToObject(sb.ToString());
        Debug.Log(sb.ToString());
    }
}
