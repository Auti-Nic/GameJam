using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int gameSceneIndex;
        [SerializeField] private int menuSceneIndex;
        [SerializeField] private GameObject endGameDialogue;

        private void Start()
        {
            endGameDialogue.SetActive(false);
        }

        public void EndGame()
        {
            Time.timeScale = 0;
            endGameDialogue.SetActive(true);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            
            SceneManager.LoadScene(gameSceneIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }
}