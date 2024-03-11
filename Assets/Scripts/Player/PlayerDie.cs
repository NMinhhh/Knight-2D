using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject dieGo;
    [SerializeField] private Button btnBorn;
    [SerializeField] private int coinToBorn;
    [SerializeField] private Text textCoin;
    private bool isLoss;
    private int mutiply;
    private Player player;
    void Start()
    {
        mutiply = 1;
        player = GetComponent<Player>();
        btnBorn.onClick.AddListener(() => { Born(); });
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDie && !isLoss)
        {
            isLoss = true;
            CanvasManager.Instance.OpenUI(dieGo);
            Time.timeScale = 0;
        }
        textCoin.text = (coinToBorn * mutiply).ToString();
    }

    public void Born()
    {
        Time.timeScale = 1;
        GameManager.Instance.UseCoins(coinToBorn * mutiply);
        mutiply += 2;
        CanvasManager.Instance.CloseUI(dieGo);
        player.Born();
        isLoss = false;
    }
}
