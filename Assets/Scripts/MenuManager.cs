using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text mainText; 
    public Text topScoreText; 
    private void Start()
    {
        if (PlayerPrefs.HasKey("IsNewGame"))
        {
            float score = ScoreManager.Instance.GameScore;
            mainText.text = "Score: " + Mathf.Round(score).ToString(CultureInfo.InvariantCulture);

            // ScoreManager.Instance.UpdateTopScore();
            // topScoreText.text = "Top Score: " + Mathf.Round(ScoreManager.TopScore).ToString(CultureInfo.InvariantCulture);
            
            PlayerPrefs.DeleteKey("IsNewGame");
        }
        else
        {
            mainText.text = "Doodle jump in Unity";
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("IsNewGame", 1);
        SceneManager.LoadScene("Game");
    }
}
