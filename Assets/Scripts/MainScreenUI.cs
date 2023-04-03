using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static MainManager;
using System.IO;

namespace Assets.Scripts
{
    public class MainScreenUI : MonoBehaviour
    {
        public InputField textbox;

        public string playerName;
        public List<PlayerData> bestScores = new List<PlayerData>();
        public static MainScreenUI Instance;
        private void Awake()
        {
            // start of new code
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code
            //if (Instance == null)
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadResults();
        }
        public void StartGame()
        {
            this.playerName = textbox.text;
            SceneManager.LoadScene(1);
        }
        public void ViewHighScores()
        {
            SceneManager.LoadScene(2);
        }
        public void SaveResults()
        {
            PlayerData data = new PlayerData()
            {
                playerName = playerName,
                score = MainManager.Instance.score
            };

            bestScores.Add(data);
            bestScores.OrderBy(s => s.score);

            string json = string.Empty;
            List<string> _aux = new List<string>();
            foreach (PlayerData item in bestScores)
                _aux.Add(JsonUtility.ToJson(item));

            json = string.Format("[{0}]", _aux.Aggregate((p, n) => p + "," + n ));

            File.WriteAllText(Application.persistentDataPath + "/scores.json", json);

            LoadResults();
        }
        public void LoadResults()
        {
            try
            {
                string path = Application.persistentDataPath + "/scores.json";
                if (File.Exists(path))
                {
                    bestScores.Clear();
                    string json = File.ReadAllText(path);
                    json = json.Replace("[", "").Replace("]", "");
                    string[] _v = json.Split(new string[] { "}" }, System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in _v)
                        bestScores.Add(JsonUtility.FromJson<PlayerData>(item.Replace(",{", "{") + "}"));

                    bestScores = bestScores.OrderByDescending(s => s.score).ToList(); ;
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        //I don't really feel confortable writing classes inside anoter class file, but I try to keep the course structure
        [System.Serializable]
        public class PlayerData
        {
            public string playerName;
            public int score;
        }
    }
}
