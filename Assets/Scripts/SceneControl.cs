using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// SceneControl.cs: SceneControl class
public class SceneControl : MonoBehaviour
{
    private ScoreCounter score_counter = null;
    public enum STEP
    {
        NONE = -1, // 상태 정보 없음.
        PLAY = 0, // 플레이 중.
        CLEAR, // 클리어.
        NUM, // 상태의 종류가 몇 개인지 나타낸다(= 2).
    };
    public STEP step = STEP.NONE; // 현재 상태.
    public STEP next_step = STEP.NONE; // 다음 상태.
    public float step_timer = 60.0f; // 경과 시간.
    private float clear_time = 0.0f; // 클리어 시간.
    public GUIStyle guistyle; // 폰트 스타일.

    private BlockRoot block_root = null;
    void Start()
    {
        // BlockRoot 스크립트를 가져온다.
        this.block_root = this.gameObject.GetComponent<BlockRoot>();
        // BlockRoot 스크립트의 initialSetUp()을 호출한다.
        this.block_root.create(); // create() 메서드에서 초기 설정
        this.block_root.initialSetUp();
        // ScoreCounter 가져오기
        this.score_counter = this.gameObject.GetComponent<ScoreCounter>();
        this.next_step = STEP.PLAY; // 다음 상태를 '플레이 중'으로.
        this.guistyle.fontSize = 24; // 폰트 크기를 24로.
    }

    bool isGameClear()
    {
        return step_timer <= 0.0f;
    }

    void Update()
    {
        this.step_timer -= Time.deltaTime;
        // 상태 변화 대기 -----.
        if (this.next_step == STEP.NONE)
        {
            switch (this.step)
            {
                case STEP.PLAY:
                    // 클리어 조건을 만족하면.
                    if(isGameClear()) {
                        this.next_step = STEP.CLEAR; // 클리어 상태로 이행.
                    }
                    break;
            }
        }
        // 상태가 변화했다면 ------.
        while (this.next_step != STEP.NONE)
        {
            this.step = this.next_step;
            this.next_step = STEP.NONE;
            switch (this.step)
            {
                case STEP.CLEAR:
                    // block_root를 정지.
                    this.block_root.enabled = false;
                    // 경과 시간을 클리어 시간으로 설정.
                    this.clear_time = this.step_timer;
                    break;
            }
        }

        switch (step)
        {
            case STEP.CLEAR:
                SceneManager.LoadScene("result");
                break;
        }

    }
    
}