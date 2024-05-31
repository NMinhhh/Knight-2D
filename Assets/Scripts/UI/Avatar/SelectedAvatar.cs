using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAvatar : MonoBehaviour
{
    [Header("Content")]

    [SerializeField] private GameObject ItemTemplate;
    [SerializeField] private Transform contentScrollView;

    AvatarData avatarData;

    private GameObject go;
    private Button btn;
    [Space]

    [Header("Selected Border")]
    [SerializeField] private GameObject border;
    [Space]

    [Header("Selected Avatar Image Current")]
    [SerializeField] private Image avatar;

    private int selectedID;

    [Header("Tab Avatar")]
    [SerializeField] private GameObject tabAvatarObj;

    void Start()
    {
        avatarData = GameData.Instance.GetAvatarData();
        GenerateAvatarUI();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentScrollView.GetComponent<GridLayoutGroup>().GetComponent<RectTransform>());
        selectedID = GameManager.Instance.GetSelectedAvatarIndex();
        SetAvatar(selectedID);
    }


    public void GenerateAvatarUI()
    {
        int count = contentScrollView.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(contentScrollView.GetChild(i).gameObject);
        }
        for(int i = 0; i < GameManager.Instance.GetAllAvatarPurchasedIndex().Count; i++)
        {
            int idx = i;
            int idxAvatarPurchased = GameManager.Instance.GetAvatarPurchased(i);
            Avatar avatar = avatarData.GetAvatar(idxAvatarPurchased);
            go = Instantiate(ItemTemplate, contentScrollView);
            btn = go.transform.GetComponent<Button>();
            btn.image.sprite = avatar.image;
            btn.onClick.AddListener(() => SelectedPoint(idx));
            btn.onClick.AddListener(() => SelectedAvatarIndex(idxAvatarPurchased));
        }
    }

    void SetAvatar(int id)
    {
        avatar.sprite = avatarData.GetAvatar(id).image;
    }

    public void SelectedClick()
    {
        SetAvatar(selectedID);
    }

    void SelectedAvatarIndex(int id)
    {
        selectedID = id;
        GameManager.Instance.ChangeAvatarIndex(selectedID);
    }

    void SelectedPoint(int id)
    {
        if (!border.activeInHierarchy)
        {
            border.SetActive(true);
            border.transform.localPosition = CellPoint(id);
        }
        else
        {
            StartCoroutine(Move(id));
        }
    }

    Vector3 CellPoint(int id)
    {
        return contentScrollView.GetChild(id).GetComponent<RectTransform>().localPosition;
    }

    IEnumerator Move(int id)
    {
        float time = 0;
        float decreaseTime = 0.25f;
        while (time < decreaseTime)
        {
            time += Time.deltaTime;
            border.transform.localPosition = Vector3.Lerp(border.transform.localPosition, CellPoint(id), time / decreaseTime);
            yield return null;
        }
    }

    public void OpenTabUI()
    {
        tabAvatarObj.SetActive(true);
    }

    public void CloseTabUI()
    {
        border.SetActive(false);
        tabAvatarObj.SetActive(false);
    }

}

