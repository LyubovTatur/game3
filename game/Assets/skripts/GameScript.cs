using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;



public class GameScript : MonoBehaviour
{
    public SavingValues savingValues;
    public GameObject playButton;
    public GameObject misses;
    public GameObject points;
    public GameObject gameOver;
    public GameObject coins;
    public Camera cam;
    public Sprite cat;
    Animator anim;
    public Sprite bonkedCat;
    public GameObject movingObject;
    private float currentSpeed = 1000;
    public GameObject[,] catsGameObjs = new GameObject[3, 3];
    public bool[,] catsGameObjsIsActive = new bool[3, 3];
    private bool IsGameGoing = false;
    private bool IsGameStarted = false;
    private int money = 0;
    private int missCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.dataPath + "/save.gamesave"))
        {
            string jsonText = File.ReadAllText(Application.dataPath + "/save.gamesave");
            savingValues = JsonUtility.FromJson<SavingValues>(jsonText);
            print($"{Application.dataPath + "/save.gamesave"} exist");
        }
        else
        {
            savingValues = new SavingValues(0, 0, new List<FixedObjects>(), new List<ItemIdCount>());
            File.WriteAllText(Application.dataPath + "/save.gamesave", JsonUtility.ToJson(savingValues));

        }
        coins.GetComponent<Text>().text = savingValues.coins.ToString();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                catsGameObjs[i, j] = GameObject.Find($"{i}{j}").transform.GetChild(0).gameObject;
                catsGameObjsIsActive[i, j] = false;
                catsGameObjs[i, j].SetActive(false);
            }
        }
        playButton.GetComponent<Button>().onClick.AddListener(delegate { StartGame(); });

    }
    public void WasBonked()
    {
        print("ouch");
        points.GetComponent<Text>().text = (int.Parse(points.GetComponent<Text>().text) + 10).ToString();
    }
    private void StartGame()
    {
        if (!IsGameStarted)
        {
            gameOver.gameObject.SetActive(false);
            IsGameStarted = true;


            print("Game is started!");
            points.GetComponent<Text>().text = "0";
            missCount = 5;
            for (int i = 0; i < misses.transform.childCount; i++)
            {
                misses.transform.GetChild(i).gameObject.SetActive(true);
            }
            //misses.gameObject.SetActive(true);
            movingObject.gameObject.SetActive(true);
            IsGameGoing = true;
        }
    }
    private async void GameStep()
    {
        IsGameGoing = false;
        bool IsFreePlase = true;
        int i = -1;
        int j = -1;
        while (IsFreePlase)
        {
            i = UnityEngine.Random.Range(0, 3);
            j = UnityEngine.Random.Range(0, 3);
            if (!catsGameObjsIsActive[i, j])
            {
                IsFreePlase = false;
                catsGameObjsIsActive[i, j] = true;
                catsGameObjs[i, j].SetActive(true);
            }

        }
        await Task.Delay(TimeSpan.FromMilliseconds(currentSpeed));
        IsGameGoing = true;
        catsGameObjsIsActive[i, j] = false;
        if (catsGameObjs[i, j].GetComponent<SpriteRenderer>().sprite == cat)
        {
            print("miss");
            if (missCount != 1)
            {
                missCount--;
                misses.transform.GetChild(missCount).gameObject.SetActive(false);
            }
            else
            {
                missCount--;
                misses.transform.GetChild(missCount).gameObject.SetActive(false);
                GameOver();
            }
        }
        catsGameObjs[i, j].GetComponent<SpriteRenderer>().sprite = cat;
        catsGameObjs[i, j].SetActive(false);

    }

    private void GameOver()
    {
        IsGameGoing = false;
        IsGameStarted = false;
        gameOver.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                catsGameObjsIsActive[i, j] = false;
                catsGameObjs[i, j].SetActive(false);
            }
        }



       
            string jsonText = File.ReadAllText(Application.dataPath + "/save.gamesave");
            savingValues = JsonUtility.FromJson<SavingValues>(jsonText);
            print($"{Application.dataPath + "/save.gamesave"} exist");
        
        savingValues.coins += int.Parse(points.GetComponent<Text>().text);
        File.WriteAllText(Application.dataPath + "/save.gamesave", JsonUtility.ToJson(savingValues));
        coins.GetComponent<Text>().text = savingValues.coins.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Input.mousePosition;
        pos = cam.ScreenToWorldPoint(pos);
        movingObject.transform.position = new Vector3(pos.x, pos.y, -8);
        movingObject.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        print(misses.transform.childCount);

        if (Input.GetMouseButtonDown(0))
        {
            print("тап");
            movingObject.transform.Rotate(0.0f, 0.0f, 45.0f);
            //if (movingObject.GetComponent<PolygonCollider2D>().IsTouching(GameObject.Find("in").GetComponent<PolygonCollider2D>()))
            //{
            //    print("boom");
            //}
            //bool isCollision = false;
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        if (movingObject.GetComponent<PolygonCollider2D>().IsTouching(catsGameObjs[i,j].gameObject.GetComponent<PolygonCollider2D>()))
            //        {
            //            isCollision = true;
            //            print("boom");
            //        }
            //    }
            //}

        }
        if (Input.GetMouseButtonUp(0))
        {
            movingObject.transform.Rotate(0.0f, 0.0f, -45.0f);
        }
        if (IsGameGoing)
        {

            //проверка есть ли еще попытки

            GameStep();
            if (currentSpeed >= 400)
            {
                currentSpeed -= 8;

            }
        }
    }


}

