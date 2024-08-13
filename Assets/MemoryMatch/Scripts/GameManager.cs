using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int timeLimit;
    public MatchItem[] matchItems;
    public MatchItemUI itemUIPb;
    public Transform gridRoot;
    public GameState state;
    private List<MatchItem> m_matchItemsCopy;
    private List<MatchItemUI> m_matchItemUIs;
    private List<MatchItemUI> m_answers;
    public float m_timeCounting;
    private int m_totalMatchItem;
    private int m_totalMoving;
    private int m_rightMoving;
    private bool m_isAnswerChecking;

    public int RightMoving { get => m_rightMoving;}
    public int TotalMoving { get => m_totalMoving;}

    public override void Awake()
    {
        MakeSingleton(false);
        m_matchItemsCopy = new List<MatchItem>();
        m_matchItemUIs = new List<MatchItemUI>();
        m_answers = new List<MatchItemUI>();
        m_timeCounting = timeLimit;
        state = GameState.Starting;
    }

    public override void Start()
    {
        base.Start();
        if (AudioController.Ins)
        AudioController.Ins.PlayBackgroundMusic();
    }

    private void Update() {

        if (state != GameState.Playing) return;

        m_timeCounting -= Time.deltaTime;
        if (m_timeCounting <= 0 && state != GameState.Timeout) {
            timeLimit = 120;
            state = GameState.Timeout;
            m_timeCounting = 0;
             if (GUIManager.Ins)
                GUIManager.Ins.timeoutDialog.Show(true);
                if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.timeOut);
            Debug.Log("Timeout!!");
        }
        if (GUIManager.Ins)
        GUIManager.Ins.UpdateTimeBar((float)m_timeCounting, (float)timeLimit);
    }

    public void PlayGame(){
        state = GameState.Playing;
        GenerateMatchItems();
        if (GUIManager.Ins)
        GUIManager.Ins.ShowGameplay(true);
    }

    private void GenerateMatchItems(){
        if (matchItems == null || matchItems.Length <= 0 || itemUIPb == null || gridRoot == null) return;
        int totalItem = matchItems.Length;
        int divItem = totalItem % 2;
        m_totalMatchItem = totalItem - divItem;
        for (int i = 0; i < m_totalMatchItem; i++) {
            var matchItem = matchItems[i];
            if (matchItem != null)
            matchItem.Id = i;
        }
        m_matchItemsCopy.AddRange(matchItems);
        m_matchItemsCopy.AddRange(matchItems);
        ShuffleMatchItems();
        ClearGrid();
        for (int i=0; i< m_matchItemsCopy.Count; i++) {
            var matchItem = m_matchItemsCopy[i];
            var matchItemUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);
            matchItemUIClone.transform.SetParent(gridRoot);
            matchItemUIClone.transform.localPosition = Vector3.zero;
            matchItemUIClone.transform.localScale = Vector3.one;
            matchItemUIClone.UpdateFirstState(matchItem.icon);//matchItem.icon
            matchItemUIClone.Id = matchItem.Id;
            m_matchItemUIs.Add(matchItemUIClone);

            if (matchItemUIClone.btnComp) {
                matchItemUIClone.btnComp.onClick.RemoveAllListeners();
                matchItemUIClone.btnComp.onClick.AddListener(() => {
                    if (m_isAnswerChecking) return;
                    m_answers.Add(matchItemUIClone);
                    matchItemUIClone.OpenAnimTrigger();
                    if (m_answers.Count == 2) {
                        m_totalMoving++;
                        m_isAnswerChecking = true;
                        StartCoroutine(CheckAnswerCo());
                    }
                    matchItemUIClone.btnComp.enabled = false;
                });
            }
        }
    }

    private IEnumerator CheckAnswerCo() {
        bool isRight = m_answers[0] != null && m_answers[1] != null
        && m_answers[0].Id == m_answers[1].Id;
        yield return new WaitForSeconds(1f);
        if (m_answers != null && m_answers.Count == 2) {
            if (isRight) {
                m_rightMoving++;
                for (int i = 0; i < m_answers.Count; i++) {
                    var answer = m_answers[i];
                    if (answer)
                    answer.ExplodeAnimTrigger();

                    if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.right);
                }
            } else {
                for (int i = 0; i < m_answers.Count; i++) {
                    var answer = m_answers[i];
                    if (answer)
                    answer.OpenAnimTrigger();

                    if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.wrong);
                }
            }
        }
        m_answers.Clear();
        m_isAnswerChecking = false;
        if (m_rightMoving == m_totalMatchItem) {

                Pref.BestMove = m_totalMoving;
                if (GUIManager.Ins)
                GUIManager.Ins.gameoverDialog.Show(true);

                if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.gameover);
            Debug.Log("Gameover!!!");
        }
    }

    private void ShuffleMatchItems(){
        if (m_matchItemsCopy == null || m_matchItemsCopy.Count <= 0) return;
        for (int i = 0; i < m_matchItemsCopy.Count; i++) {
            var temp = m_matchItemsCopy[i];
            if (temp != null) {
                int randIdx = Random.Range(0, m_matchItemsCopy.Count);
                m_matchItemsCopy[i] = m_matchItemsCopy[randIdx];
                m_matchItemsCopy[randIdx] = temp;
            }
        }
    }

    private void ClearGrid() {
        if (gridRoot == null) return;
        for (int i = 0; i < gridRoot.childCount; i++) {
            var child = gridRoot.GetChild(i);
            if (child)
            Destroy(child.gameObject);
        }
    }
}
