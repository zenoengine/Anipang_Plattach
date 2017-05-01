using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultRoot : MonoBehaviour
{

    public Text lastTotalScoreText = null;
    public Text lastMaxChainText = null;
    public Text bestTotalScoreText = null;

    void Start()
    {
        SoundManager.Instance.PlayMusic("bgm2", false);

        int lastTotalScore = PlayerPrefs.GetInt("last_total_score", 0);
        int lastMaxChain = PlayerPrefs.GetInt("last_max_chain", 0);
        int bestScore = PlayerPrefs.GetInt("best_total_score", 0);

        lastTotalScoreText.text = lastTotalScore.ToString();
        lastMaxChainText.text = lastMaxChain.ToString();
        bestTotalScoreText.text = bestScore.ToString();
    }
}
