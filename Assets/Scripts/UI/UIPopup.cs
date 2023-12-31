using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIPopup : UIBase
{
    private DataManager Data { get => GameManager.Data; }

    [Header("내용이 표시되는 오브젝트")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    private bool IsTemp;
    private float _time;
    private float _duration;

    private void Update()
    {
        _time += Time.deltaTime;
        if (IsTemp)
        {
            if (_duration < _time)
                SelfHideUI();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        var rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
        rect.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    public void Initialize(string data, string title = null, Action actAtHide = null, bool temp = false, float duration = 0.0f)
    {
        RefreshSize();
        ActAtHide = actAtHide;
        if (title == null)
        {
            _title.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _title.transform.parent.gameObject.SetActive(true);
            _title.text = title;
        }
        _description.text = data;
        IsTemp = temp;
        _time = 0.0f;
        _duration = duration;
        transform.localPosition = Vector3.zero;
    }

    public void Move(Vector3 worldPosition)
    {
        transform.localPosition = Camera.current.WorldToScreenPoint(worldPosition);
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public override void HideUI()
    {
        var rect = GetComponent<RectTransform>();
        rect.DOScale(.0f, 0.3f).SetEase(Ease.InBack);
        Invoke("CallHide", 0.4f);
    }

    private void CallHide()
    {
        base.HideUI();
    }

    protected override void SelfCloseUI()
    {
        UIManager.CloseUI(this);
    }

    protected override void SelfHideUI()
    {
        UIManager.HideUI(this);
    }

    public override void RefreshSize()
    {
        base.RefreshSize();
        _title.fontSize = _baseFontSize[0] * Data.FontSizeMultiplier;
        _description.fontSize = _baseFontSize[1] * Data.FontSizeMultiplier;
    }
}
