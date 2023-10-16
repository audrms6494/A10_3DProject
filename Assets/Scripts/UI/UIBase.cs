using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [Header("최상위 RectTransform을 지정할 것.")]
    [SerializeField] protected RectTransform _self;
    [Header("Size Options\n(0:title, 1:description)")]
    [SerializeField] protected float[] _baseFontSize;
    protected Action ActAtHide;
    protected Action ActAtClose;


    public virtual void AddActAtHide(Action action) { ActAtHide += action; }
    public virtual void AddActAtClose(Action action) { ActAtClose += action; }

    protected virtual void OnDisable()
    {
        Invoke(nameof(SelfCloseUI), UIManager.Instance.UIRemainTime);
    }

    protected virtual void OnEnable()
    {
        CancelInvoke();
    }

    protected virtual void InitialSize()
    {
        gameObject.transform.localScale = Vector3.one * UIManager.Instance.UISize;
    }

    public virtual void Refresh()
    {
        InitialSize();
    }

    /// <summary>
    /// 이 메소드를 사용하지 말 것.
    /// </summary>
    public virtual void CloseUI()
    {
        ActAtClose?.Invoke();
        Destroy(gameObject);
    }
    /// <summary>
    /// 이 메소드를 사용하지 말 것.
    /// </summary>
    public virtual void HideUI()
    {
        ActAtHide?.Invoke();
        if (gameObject != null)
            gameObject.SetActive(false);
    }

    protected virtual void SelfCloseUI()
    {
        UIManager.CloseUI(this);
    }

    protected virtual void SelfHideUI()
    {
        UIManager.HideUI(this);
    }
}
