using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MatchItemUI : MonoBehaviour
{
    private int m_id;
    public Sprite bg;
    public Sprite BackBG;
    public Image itemBG;
    public Image itemIcon;
    public Button btnComp;
    private bool m_isOpened;
    private Animator m_anim;

    public int Id { get => m_id; set => m_id = value; }

    private void Awake() {
        m_anim = GetComponent<Animator>();
    }

    public void UpdateFirstState(Sprite icon){
        if (itemBG)
        itemBG.sprite = BackBG;

        if (itemIcon)
        {
            itemIcon.sprite = icon;
            itemIcon.gameObject.SetActive(false);//false
        }
    }

    public void ChangeState() {
        m_isOpened = !m_isOpened;
        if (itemBG)
        itemBG.sprite = m_isOpened ? bg : BackBG;
        if (itemIcon)
        itemIcon.gameObject.SetActive(m_isOpened);
    }

    public void OpenAnimTrigger() {
        if (m_anim)
        m_anim.SetBool(AnimState.Flip.ToString(), true);
    }

        public void ExplodeAnimTrigger() {
        if (m_anim)
        m_anim.SetBool(AnimState.Explode.ToString(), true);
    }

        public void BackToIdle() {
        if (m_anim)
        m_anim.SetBool(AnimState.Flip.ToString(), false);

        if (btnComp)
        btnComp.enabled = !m_isOpened;
    }
}
