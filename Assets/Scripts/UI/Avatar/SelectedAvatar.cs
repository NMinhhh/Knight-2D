using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAvatar : MonoBehaviour
{
    public static SelectedAvatar Instance {  get; private set; }
    [Header("List")]
    [SerializeField] private AvatarData avatarData;

    [SerializeField] private GameObject ItemTemplate;
    [SerializeField] private Transform contentScrollView;
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
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GenerateAvatarUI();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentScrollView.GetComponent<GridLayoutGroup>().GetComponent<RectTransform>());
        selectedID = CoinManager.Instance.GetSelectedAvatarIndex();
        SetAvatar(selectedID);
    }


    public void GenerateAvatarUI()
    {
        int count = contentScrollView.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(contentScrollView.GetChild(i).gameObject);
        }
        for(int i = 0; i < CoinManager.Instance.GetAllAvatarPurchasedIndex().Count; i++)
        {
            int idx = i;
            int idxAvatarPurchased = CoinManager.Instance.GetAvatarPurchased(i);
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
        CoinManager.Instance.ChangeAvatarIndex(selectedID);
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
        tabAvatarObj.SetActive(false);
    }

}

