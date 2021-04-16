using UnityEngine;
using UnityEngine.SceneManagement;

namespace StelsTestTask.Core
{
    public class LevelBootstrapper : MonoBehaviour
    {
        public static LevelBootstrapper Instance;

        private int lastLvlIndex = 1;
        private int currentlvl;

        private const string lvlKeyword = "lvl";

        private void Awake()
        {
            Instance = this;

            lastLvlIndex = SceneManager.sceneCountInBuildSettings - 1;
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            currentlvl = PlayerPrefs.GetInt(lvlKeyword, 1);

            Replay();
        }

        public void NextLvl()
        {
            if (currentlvl + 1 <= lastLvlIndex)
            {
                currentlvl++;
            }
            else
            {
                currentlvl = 1;
            }

            PlayerPrefs.SetInt(lvlKeyword, currentlvl);
            PlayerPrefs.Save();

            SceneManager.LoadScene(currentlvl, LoadSceneMode.Single);
        }

        public void Replay()
        {
            SceneManager.LoadScene(currentlvl, LoadSceneMode.Single);
        }
    }
}