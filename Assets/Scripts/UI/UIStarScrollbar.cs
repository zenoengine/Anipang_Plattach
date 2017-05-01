using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStarScrollbar : MonoBehaviour
{
    Scrollbar mScrollbar = null;
    Text mText = null;
    BlockRoot mBlockRoot = null;

    // Use this for initialization
    void Start()
    {
        mScrollbar = GetComponent<Scrollbar>();
        mText = GameObject.Find("StarScrollbarText").GetComponent<Text>();
        mBlockRoot = GameObject.Find("GameRoot").GetComponent<BlockRoot>();
    }

    // Update is called once per frame
    void Update()
    {
        mScrollbar.size = (float)mBlockRoot.Combo / 10;

        mText.text = mBlockRoot.Combo.ToString();
    }
}
