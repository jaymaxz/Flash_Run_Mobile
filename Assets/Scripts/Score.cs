using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {


    private float score = 0.0f;

    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 20;
    private int scoreToNextLevel = 10;

    private bool isDead = false;

    public Text scoreText;
    public DeathMenu deathMenu;
	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            return;
        }

        if (score >= scoreToNextLevel)
        {
            LevelUp();
        }

        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
	}

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
    }

    public void onDeath()
    {
        isDead = true;
        if (PlayerPrefs.GetFloat("highscore") < score)
        {
            PlayerPrefs.SetFloat("highscore", score);
        }     
        deathMenu.ToggleEndMenu(score);
    }
}
