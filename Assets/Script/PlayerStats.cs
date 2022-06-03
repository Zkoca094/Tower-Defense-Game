using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    public Text moneyText;
    public Text livesText;
    public Text roundsText;
    void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }
    void Update()
    {
        moneyText.text = string.Format("${0}", Money);
        livesText.text = string.Format("{0} lives", Lives);
        int waves = GetComponent<WaveSpawner>().GetWaveIndex();
        roundsText.text = string.Format("Rounds: {0} / {1}", PlayerStats.Rounds, waves);
    }
}
