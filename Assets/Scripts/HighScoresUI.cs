using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static MainManager;
using System.IO;
using static Assets.Scripts.MainScreenUI;
using Unity.Rendering.HybridV2;

namespace Assets.Scripts
{
    public class HighScoresUI : MonoBehaviour
    {
        public Text txtNames;
        public Text txtDots;
        public Text txtScores;
        public static HighScoresUI Instance;
        private void Awake()
        {
            // start of new code
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code
            Instance = this;
        }
        private void Start()
        {
            LoadResults();
        }
        public void LoadResults()
        {
            try
            {
                string names = string.Empty;
                string scores = string.Empty;
                string dots = string.Empty;
                List<PlayerData> data = MainScreenUI.Instance.bestScores.Take(10).ToList();
                foreach (PlayerData p in data)
                {
                    names += string.Format("{0}\n", p.playerName);
                    scores += string.Format("{0}\n", p.score);
                    dots += "..........\n";
                }
                txtNames.text = names;
                txtScores.text = scores;
                txtDots.text = dots;
            }
            catch (System.Exception ex)
            {
            }
        }
        public void BackToMainScreen()
        {
            SceneManager.LoadScene(0);
        }
    }
}