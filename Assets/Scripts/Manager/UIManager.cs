using System;
using System.Collections.Generic;
using UnityEngine;


public class UIManager
{
    private static UIManager _instance;
    /// <summary>
    /// nullüũ�� �ϰ� ������ �����Ͽ� ������
    /// </summary>
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }

    private Dictionary<Type, GameObject> _prefabs;
    private LinkedList<UIBase> OpenList;
    private LinkedList<UIBase> HideList;

    public float UISize { get => GameManager.Data.UISize; }
    public float FontSize { get => GameManager.Data.FontSizeMultiplier; }
    public float UIRemainTime { get => GameManager.Data.UIRemainTime; }
    private Canvas _canvas;
    public Canvas UIRoot
    {
        get
        {
            if (_canvas == null)
                if (GameObject.Find("_UI").TryGetComponent(out _canvas))
                    return _canvas;
                else
                    return null;
            return _canvas;
        }
        set
        {
            _canvas = value;
        }
    }

    public UIManager()
    {
        _prefabs = new Dictionary<Type, GameObject>();
        OpenList = new LinkedList<UIBase>();
        HideList = new LinkedList<UIBase>();
        //LoadUIPrefabs();
    }
    /// <summary>
    /// Open ����Ʈ�� ù��°�� ��ġ�� UI�� Hide�ϸ�, �̹� Hide�� ��쿡�� �ƹ��͵� ���� ����
    /// </summary>
    public static void HideTopUI()
    {
        if (Instance.OpenList.Count > 0)
        {
            HideUI(Instance.OpenList.First.Value);
        }
    }
    /// <summary>
    /// �޼ҵ� ������ ���� �ش� Ŭ������ ĳ�����Ͽ� Initialize�Ͽ� ����� ��
    /// </summary>
    /// <param name="type">UI Type�� ���� �������� �޶���</param>
    /// <param name="root">�θ� canvas/UI�� �ǹ���</param>
    /// <returns>�ʿ��� Ŭ������ ĳ�����Ͽ� ����� ��</returns>
    public static UIBase ShowUI(eUIType type, RectTransform root = null)
    {
        switch (type)
        {
            case eUIType.Popup:
                return ShowUI<UIPopup>(root);
            default:
                return null;
        }
    }
    /// <summary>
    /// �ش� ��ũ��Ʈ�� �ٿ��� �������� �ҷ���, Hide�� ���ӿ�����Ʈ�� �ִ� ��쿡�� �ش� ���ӿ�����Ʈ�� ������
    /// </summary>
    /// <typeparam name="T">�����鿡 �پ��ִ� Ŭ����</typeparam>
    /// <param name="root">�θ� canvas/UI�� �ǹ���</param>
    /// <returns>�ش� �������� ������ null�� ������</returns>
    public static T ShowUI<T>(RectTransform root = null) where T : UIBase
    {
        var open = GetHideUI<T>();
        if (open != null)
        {
            Instance.HideList.Remove(open);
            if (root == null)
                open.transform.SetParent(Instance.UIRoot.transform);
            else
                open.transform.SetParent(root);

            open.gameObject.SetActive(true);
            return open;
        }

        if (!Instance._prefabs.ContainsKey(typeof(T)))
            Instance.LoadUIPrefab(typeof(T).Name);

        var prefab = Instance._prefabs[typeof(T)];
        if (prefab != null)
        {
            GameObject obj;
            if (root == null)
                obj = GameObject.Instantiate(prefab, Instance.UIRoot.transform);
            else
                obj = GameObject.Instantiate(prefab, root);
            var uiClass = obj.GetComponent<UIBase>();

            Instance.OpenList.AddFirst(uiClass);

            obj.SetActive(true);
            return uiClass as T;
        }
        else
            return null;
    }
    /// <summary>
    /// ���� ������Ʈ�� ����
    /// </summary>
    public static void CloseUI<T>(T target) where T : UIBase
    {
        if (IsHide(target))
        {
            target.CloseUI();
            Instance.OpenList.Remove(target);
            Instance.HideList.Remove(target);
        }
        else
        {
            target.CloseUI();
            Instance.OpenList.Remove(target);
        }
    }
    /// <summary>
    /// ���� ������Ʈ�� ��Ȱ��ȭ
    /// </summary>
    public static void HideUI<T>(T target) where T : UIBase
    {
        if (!IsHide(target))
        {
            target.HideUI();
            Instance.HideList.AddLast(target);
            Instance.OpenList.Remove(target);
            Instance.OpenList.AddLast(target);
        }
    }
    /// <summary>
    /// �ش� UI�� Open List�� �ִ��� Ȯ���ϴ� �޼ҵ��, Hide ������ �˷����� �ʴ´�.
    /// Ȱ��ȭ ���´� activeInHierarchy�� ���� �� �� �ְ�, ����ϱ� ���ؼ��� ShowUI(eUIType type)�� �θ��� �ȴ�.
    /// </summary>
    /// <returns>ã�� �� ������ null�� ������</returns>
    public static T GetOpenUI<T>(T search) where T : UIBase
    {
        foreach (var ui in Instance.OpenList)
        {
            if (ui == search)
                return ui as T;
        }
        return null;
    }
    /// <summary>
    /// ���� �ش� UI Type�� Open List�� �ִ��� Ȯ���ϴ� �޼ҵ��, Hide ������ �˷����� �ʴ´�.
    /// Ȱ��ȭ ���´� activeInHierarchy�� ���� �� �� �ְ�, ����ϱ� ���ؼ��� ShowUI(eUIType type)�� �θ��� �ȴ�.
    /// </summary>
    /// <returns>ã�� �� ������ null�� ������</returns>
    public static T GetOpenUI<T>() where T : UIBase
    {
        LinkedListNode<UIBase> ui = Instance.OpenList.First;
        while (ui != null)
        {
            if (ui.Value is T)
                return ui.Value as T;
            ui = ui.Next;
        }
        return null;
    }
    /// <summary>
    /// �ش� UI�� Hide List�� �ִ��� Ȯ���ϴ� �޼ҵ�
    /// </summary>
    /// <returns>ã�� �� ������ null�� ������</returns>
    public static T GetHideUI<T>(T search) where T : UIBase
    {
        foreach (var ui in Instance.HideList)
        {
            if (ui == search)
                return ui as T;
        }
        return null;
    }
    /// <summary>
    /// Hide�� �ش� UI Type�� Hide ����Ʈ�� �ִ��� Ȯ���ϴ� �޼ҵ�
    /// </summary>
    /// <returns>ã�� �� ������ null�� ������</returns>
    public static T GetHideUI<T>() where T : UIBase
    {
        LinkedListNode<UIBase> ui = Instance.HideList.First;
        while (ui != null)
        {
            if (ui.Value is T)
                return ui.Value as T;
            ui = ui.Next;
        }
        return null;
    }
    /// <summary>
    /// Open List�� �ִ� ��� UI ���ӿ�����Ʈ�� �����Ѵ�.
    /// </summary>
    public static void CloseAllOpenUI()
    {
        foreach (var ui in Instance.OpenList)
        {
            ui.CloseUI();
        }
        Instance.OpenList.Clear();
        Instance.HideList.Clear();
    }
    /// <summary>
    /// Hide List�� �ִ� ��� UI ���ӿ�����Ʈ�� �����Ѵ�.
    /// </summary>
    public static void CloseAllHideUI()
    {
        foreach (var ui in Instance.HideList)
        {
            ui.CloseUI();
            Instance.OpenList.Remove(ui);
        }
        Instance.HideList.Clear();
    }
    /// <summary>
    /// �ش� UI type�� Open List�� ���ԵǾ� �ֳ� Ȯ���Ѵ�. IsHide�� ��쿡 ���� �� �����ϴ�.
    /// </summary>
    public static bool IsOpen(eUIType type)
    {
        switch (type)
        {
            case eUIType.Popup:
                return IsOpen<UIPopup>();
            default:
                return false;
        }
    }

    private static bool IsOpen<T>() where T : UIBase
    {
        foreach (var ui in Instance.OpenList)
        {
            if (ui is T)
                return true;
        }
        return false;
    }
    /// <summary>
    /// �ش� UI�� Open List�� ���ԵǾ� �ֳ� Ȯ���Ѵ�.
    /// </summary>
    public static bool IsOpen<T>(T target) where T : UIBase
    {
        foreach (var ui in Instance.OpenList)
        {
            if (ui == target)
                return true;
        }
        return false;
    }
    /// <summary>
    /// �ش� UI type�� Hide List�� ���ԵǾ� �ֳ� Ȯ���Ѵ�.
    /// </summary>
    public static bool IsHide(eUIType type)
    {
        switch (type)
        {
            case eUIType.Popup:
                return IsHide<UIPopup>();
            default:
                return false;
        }
    }
    private static bool IsHide<T>() where T : UIBase
    {
        foreach (var ui in Instance.HideList)
        {
            if (ui is T)
                return true;
        }
        return false;
    }
    /// <summary>
    /// Hide List�� ���ԵǾ� �ֳ� Ȯ���Ѵ�.
    /// </summary>
    public static bool IsHide<T>(T target) where T : UIBase
    {
        foreach (var ui in Instance.HideList)
        {
            if (ui == target)
                return true;
        }
        return false;
    }

    private void LoadUIPrefabs()
    {
        var objs = Resources.LoadAll<GameObject>("Prefabs/UI/");
        foreach (var obj in objs)
        {
            var type = obj.GetComponent<UIBase>().GetType();
            _prefabs.Add(type, obj);
            Debug.Log($"{type}(Prefabs/UI/{obj.name}) is loaded.");
        }
    }

    private void LoadUIPrefab(string name)
    {
        var obj = Resources.Load<GameObject>("Prefabs/UI/" + name);
        if (obj != null)
        {
            var type = obj.GetComponent<UIBase>().GetType();
            _prefabs.Add(type, obj);
            Debug.Log($"{type}(Prefabs/UI/{obj.name}) is loaded.");
        }
    }
}
