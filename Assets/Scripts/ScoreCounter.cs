using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {
    public struct Count
    {
        public int ignite; // 연쇄 수
        public int score; // 점수
        public int total_socre; // 합계 점수
    };
    public Count last; // 마지막(이번) 점수
    public Count best; // 최고 점수.
    public static int QUOTA_SCORE = 1000; // 클리어 하는 데 필요한 점수.
    public GUIStyle guistyle; // 폰트 스타일.
    void Start()
    {
        this.last.ignite = 0;
        this.last.score = 0;
        this.last.total_socre = 0;
        this.guistyle.fontSize = 16;
    }
    // 화면에 텍스트와 이미지 표시
    void OnGUI()
    {
        int x = 20;
        int y = 50;
        GUI.color = Color.black;
        this.print_value(x + 20, y, "연쇄 카운트", this.last.ignite);
        y += 30;
        this.print_value(x + 20, y, "가산 스코어", this.last.score);
        y += 30;
        this.print_value(x + 20, y, "합계 스코어", this.last.total_socre);
        y += 30;
    }

    public void print_value(int x, int y, string label, int value)
    {
        // label을 표시.
        GUI.Label(new Rect(x, y, 100, 20), label, guistyle);
        y += 15;
        // 다음 행에 value를 표시.
        GUI.Label(new Rect(x + 20, y, 100, 20), value.ToString(), guistyle);
        y += 15;
    }
    // 연쇄 횟수를 가산
    public void addIgniteCount(int count)
    {
        this.last.ignite += count; // 연쇄 수에 count를 합산.
        this.update_score(); // 점수 계산.
    }
    // 연쇄 횟수를 리셋
    public void clearIgniteCount()
    {
        this.last.ignite = 0; // 연쇄 횟수 리셋.
    }
    // 더해야 할 점수를 계산
    private void update_score()
    {
        this.last.score = this.last.ignite * 10; // 점수 갱신.
    }
    // 합계 점수를 갱신
    public void updateTotalScore()
    {
        this.last.total_socre += this.last.score;
    }
    // 게임을 클리어했는지 판정 (SceneControl에서 사용)
    public bool isGameClear()
    {
        bool is_clear = false;
        // 현재 합계 점수가 클리어 기준보다 크면.
        if (this.last.total_socre > QUOTA_SCORE)
        {
            is_clear = true;
        }
        return (is_clear);
    }
}
