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
    private bool isChooseBorder;
    [Space]

    [Header("Selected Avatar Image Current")]
    [SerializeField] private Image avatar;

    private int selectedID;
    private int selectedPoint;

    [Header("Tab Avatar")]
    [SerializeField] private GameObject tabAvatarObj;

    void Start()
    {
        avatarData = GameData.Instance.GetAvatarData();
        GenerateAvatarUI();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentScrollView.GetComponent<GridLayoutGroup>().GetComponent<RectTransform>());
        selectedID = GameManager.Instance.GetSelectedAvatarID();
        SetAvatar(selectedID);

        StartCoroutine(Move(0));
    }

    private void Update()
    {
        if (isChooseBorder)
        {
            if (Vector3.Distance(border.transform.localPosition, CellPoint(selectedPoint)) <= .1f)
            {
                isChooseBorder = false;
                GameObject go = contentScrollView.GetChild(selectedPoint).gameObject;
                go.transform.GetChild(1).gameObject.SetActive(true);
                border.SetActive(false);
            }
        }
    }

    public void ChangeAvatar()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        SetAvatar(selectedID);
    }

    void SetAvatar(int id)
    {
        GameManager.Instance.ChangeAvatarID(id);
        avatar.sprite = avatarData.GetAvatar(id).image;
    }

    public void GenerateAvatarUI()
    {
        int count = contentScrollView.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(contentScrollView.GetChild(i).gameObject);
        }
        for(int i = 0; i < GameManager.Instance.GetAllAvatarPurchased().Count; i++)
        {
            int idx = i;
            int idxAvatarPurchased = GameManager.Instance.GetAvatarPurchased(i);
            Avatar avatar = avatarData.GetAvatar(idxAvatarPurchased);
            go = Instantiate(ItemTemplate, contentScrollView);
            go.transform.GetChild(0).GetComponent<Image>().sprite = avatar.image;
            btn = go.transform.GetComponent<Button>();
            btn.onClick.AddListener(() => SelectedPoint(idx));
            btn.onClick.AddListener(() => SelectedAvatarID(idxAvatarPurchased));
        }
    }

    void SelectedAvatarID(int id)
    {
        selectedID = id;
    }

    void SelectedPoint(int id)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        contentScrollView.GetChild(selectedPoint).GetChild(1).gameObject.SetActive(false);
        border.SetActive(true);
        selectedPoint = id;
        StartCoroutine(Move(id));
    }

    IEnumerator Move(int id)
    {
        isChooseBorder = true;
        float time = 0;
        float decreaseTime = 0.25f;
        while (time < decreaseTime)
        {
            time += Time.deltaTime;
            border.transform.localPosition = Vector3.Lerp(border.transform.localPosition, CellPoint(id), time / decreaseTime);
            yield return null;
        }
    }

    Vector3 CellPoint(int id)
    {
        return contentScrollView.GetChild(id).GetComponent<RectTransform>().localPosition;
    }

    public void OpenTabUI()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        tabAvatarObj.SetActive(true);
    }

    public void CloseTabUI()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        border.SetActive(false);
        isChooseBorder = false;
        tabAvatarObj.SetActive(false);
    }

}

