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
        selectedID = GameManager.Instance.GetSelectedAvatarID();
        SetAvatar(selectedID);
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

    void SelectedPoint(int i)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        contentScrollView.GetChild(selectedPoint).GetChild(1).gameObject.SetActive(false);
        contentScrollView.GetChild(i).GetChild(1).gameObject.SetActive(true);
        selectedPoint = i;
    }

    public void OpenTabUI()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        tabAvatarObj.SetActive(true);
    }

    public void CloseTabUI()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        tabAvatarObj.SetActive(false);
    }

}

