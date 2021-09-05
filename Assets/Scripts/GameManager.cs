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
            SceneManager.LoadScene(gameSceneIndex);
        }
    }
}