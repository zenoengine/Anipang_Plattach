using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : MonoBehaviour {
    Text mText = null;
    ScoreCounter mScoreCounter = null;
	// Use this for initialization
	void Start () {
        mText = GetComponent<Text>();
        mScoreCounter = GameObject.Find("GameRoot").GetComponent<ScoreCounter>();
    }
	
	// Update is called once per frame
	void LateUpdate() {
        mText.text = mScoreCounter.last.total_socre.ToString();
    }
}
