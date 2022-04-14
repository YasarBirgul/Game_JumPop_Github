using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score OG2D;
    public static int score;
    public int highScore;
    
    void Start()
    {
        
        OG2D = this;
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore1", 0);
    }

    private void OnGUI()
    {   GUIStyle secondStyle = new GUIStyle();
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 80;
        myStyle.normal.textColor = Color.white;
        secondStyle.fontSize = 20;
        secondStyle.normal.textColor =Color.white;
        GUI.Label(new Rect(50f,50f,100f,200f),"" + score,myStyle);
        GUI.Label(new Rect(50f,150f,100f,200f), "Highest Score " + highScore,secondStyle);
        
    }

    public void CheckHighScore()
    {
        if (score > highScore)
        {
            Debug.Log("Saving Score");
            PlayerPrefs.SetInt("HighScore1",score);
        }
    }
}
