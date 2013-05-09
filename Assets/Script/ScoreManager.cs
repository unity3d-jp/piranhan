using UnityEngine;
using System.Collections;

public class ScoreManager : MonoSingleton<ScoreManager>
{
	private  int m_Score;
	private  int m_HighScore;

	public int Score {
		get{ return m_Score;}
		set { m_Score = value;}
	}

	public  int HighScore {
		get{ return m_HighScore;}
		set { 
			PlayerPrefs.SetInt ("highscore", value);
			m_HighScore = value;
		}
	}

	public void AddScore (int point)
	{
		instance.Score += point;
		instance.HighScore = Mathf.Max (instance.Score, instance.HighScore);
	}

	public void Reset ()
	{
		instance.Score = 0;
	}
}
