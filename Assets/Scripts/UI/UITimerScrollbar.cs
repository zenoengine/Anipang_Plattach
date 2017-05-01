using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UITimerScrollbar : MonoBehaviour {
    Scrollbar mScrollbar = null;
    Text mText = null;
    SceneControl mSceneControl = null;

	// Use this for initialization
	void Start () {
        mScrollbar = GetComponent<Scrollbar>();
        mText = GameObject.Find("TimerScrollbarText").GetComponent<Text>();
        mSceneControl = GameObject.Find("GameRoot").GetComponent<SceneControl>();
    }
	
	// Update is called once per frame
	void Update () {
        mScrollbar.size = mSceneControl.step_timer / 60;
        mText.text = Mathf.CeilToInt(mSceneControl.step_timer).ToString();
    }
}
