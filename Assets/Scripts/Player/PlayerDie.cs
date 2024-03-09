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
    }

    // Update is called once per frame
    void Update()
    {

        if(player.isDie && !isLoss)
        {
            isLoss = true;
            CanvasManager.Instance.OpenUI(dieGo);
            Time.timeScale = 0;
            btnBorn.onClick.AddListener(() => { Born(); });
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
