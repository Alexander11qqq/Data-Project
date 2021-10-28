using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class PlayerData
{

    public string BestScoreName;
    public int BestScore;

}

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    public Text BestScoreText;
    public string NewPlayerName;
    public string BestScoreName;
    public int BestScore;
 
    private void Awake()
    {

        //Начало нового кода
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //Конец нового кода

        Instance = this;
        //Не уничтожайте целевой объект при загрузке новой сцены.
        DontDestroyOnLoad(gameObject);

        LoadGame();
        BestScoreText.text = "Best Score: " + BestScoreName + " " + BestScore;

    }

        public void StartGame()
    {

        SceneManager.LoadScene(1);

    }

        public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void GetPlayerName(string name)
    {

        NewPlayerName = name;
        Debug.Log(NewPlayerName);

    }

    public void SaveGame()
    {

        PlayerData saveData = new PlayerData();
        saveData.BestScoreName = BestScoreName;
        saveData.BestScore = BestScore;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);

    }

    public void LoadGame()
    {

        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            BestScoreName = loadedData.BestScoreName;
            BestScore = loadedData.BestScore;

        }

    }
}
