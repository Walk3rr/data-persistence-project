using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text RecordHolder;
    public Text Name;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int highscore;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    
    void Start()
    {
        ApplyName();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        LoadHighscoreInfo();
        DisplayHighScoreAndName();
        if (!m_Started)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void ApplyName()
    {
        Name.text = "Name: " + MenuManager.Instance.PlayerName;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }




     [System.Serializable]
    /*public class SaveThisFile
    {
        public string name;
        public int score;
    }

    public void LoadHighscoreInfo()
    {
        string filename = Application.persistentDataPath + "/savesthisjson.json";
        if(File.Exists(filename))
        {
            SaveThisFile HscoreNameReal = JsonUtility.FromJson<SaveThisFile>(File.ReadAllText(Application.persistentDataPath + "/savesthisjson.json"));
            RecordHolder.text = "Best Score" + HscoreNameReal.name +" "+ HscoreNameReal.score;
            highscore = HscoreNameReal.score;
        }

    }

    public void DisplayHighScoreAndName()
    {
        if(m_Points > highscore)
        {
            SaveThisFile HscoreNameReal = new SaveThisFile();

            HscoreNameReal.name = Name.text;
            HscoreNameReal.score = m_Points;

            string writable = JsonUtility.ToJson(HscoreNameReal);

            File.WriteAllText(Application.persistentDataPath + "/savesthisjson.json",writable);
        }
    } */
    public class SaveFile
    {
        public string name;
        public int score;
    }

    public void LoadHighscoreInfo()
    {
        string filename = Application.persistentDataPath + "/savesjson.json";
        if(File.Exists(filename))
        {
            SaveFile HscoreName = JsonUtility.FromJson<SaveFile>(File.ReadAllText(Application.persistentDataPath + "/savesjson.json"));
            RecordHolder.text = "Best Score: " + HscoreName.name +" - "+ HscoreName.score;
            highscore = HscoreName.score;
        }

    }

    public void DisplayHighScoreAndName()
    {
        if(m_Points > highscore)
        {
            SaveFile HscoreName = new SaveFile();

            HscoreName.name = Name.text;
            HscoreName.score = m_Points;

            string writable = JsonUtility.ToJson(HscoreName);

            File.WriteAllText(Application.persistentDataPath + "/savesjson.json",writable);
        }
    }

    



    
}
