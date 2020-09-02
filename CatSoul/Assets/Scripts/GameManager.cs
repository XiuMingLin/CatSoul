using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isRuning = false;
    
    public GameObject border;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRuning)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        border.SetActive(true);
        isRuning = true;
        // 先设置为从0，0，0生成
        // 之后改成选定位置生成
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
