using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUpgrade : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int money;
    [SerializeField] int money_click;
    [SerializeField] int passive_click;
    [SerializeField] int[] activePref;
    public Text moneyText;

    [Header("Audio")]
    [SerializeField] AudioSource buttonsClick;
    [SerializeField] AudioSource upgradeClick;

    [Header("Upgrade")]
    public int[] CostClick;
    public Text[] CostText;
    [SerializeField] GameObject[] soldText;
    [SerializeField] GameObject[] buttonsOff;

    [Header("Programing Upgrade")]
    [SerializeField]int progressProgram = 0;
    [SerializeField] Text progressText;
    [SerializeField]int maxProgress = 5;
    private Save sv = new Save();

    [Header("Prefabs")]
    [SerializeField] GameObject[] lampPref;
    [SerializeField] GameObject[] compPref;
    [SerializeField] GameObject[] monitorPref;
    [SerializeField] GameObject[] tablePref;
    [SerializeField] GameObject[] chairPref;
    [SerializeField] GameObject[] posterPref;

    void Start()
    {
        money = PlayerPrefs.GetInt("money");
        money_click = PlayerPrefs.GetInt("money_click");
        passive_click = PlayerPrefs.GetInt("passive_click");
        progressProgram = PlayerPrefs.GetInt("progressProgram");
        maxProgress = PlayerPrefs.GetInt("maxProgress");

        PrefStart();
    }

    private void PrefStart()
    {
        if (activePref[0] < lampPref.Length - 1)
        {
            lampPref[activePref[0] + 1].gameObject.SetActive(true);
        }
        else
        {
            buttonsOff[0].gameObject.SetActive(false);
            soldText[0].gameObject.SetActive(true);
        }

        if (activePref[1] < compPref.Length - 1)
        {
            compPref[activePref[1] + 1].gameObject.SetActive(true);
        }
        else
        {
            buttonsOff[1].gameObject.SetActive(false);
            soldText[1].gameObject.SetActive(true);
        }

    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));

            for (int i = 0; i < 6; i++)
            {
                CostClick[i] = sv.CostClick[i];
                CostText[i].text = sv.CostClick[i] + "$";
                activePref[i] = sv.activePref[i];
            }
        }
    }

    void Update()
    {
        moneyText.text = "Money: " + money.ToString() + "$";
        SaveCostShop();
        progressText.text = progressProgram.ToString() + "/" + maxProgress.ToString();

    }

    public void UpdateProgress()
    {
        if (progressProgram < maxProgress)
        {
            progressProgram++;
            PlayerPrefs.SetInt("progressProgram", progressProgram);

        }
        if (progressProgram == maxProgress)
        {
            progressProgram = 0;
            maxProgress += 5;
            PlayerPrefs.SetInt("progressProgram", progressProgram);
            PlayerPrefs.SetInt("maxProgress", maxProgress);

        }
    }
    public void UpLamp()
    {
        if(money >= CostClick[0] && activePref[0] < lampPref.Length - 2)
        {
            upgradeClick.Play();

            money -= CostClick[0];
            CostClick[0] *= 2;
            money_click += 5;
            passive_click += 5;
            CostText[0].text = CostClick[0] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            activePref[0]++;
            lampPref[activePref[0]].gameObject.SetActive(false);
            lampPref[activePref[0] + 1].gameObject.SetActive(true);
        }
        else if(activePref[0] == lampPref.Length - 2)
        {
            upgradeClick.Play();
            activePref[0]++;
            lampPref[activePref[0]].gameObject.SetActive(false);
            CostText[0].gameObject.SetActive(false);
            soldText[0].gameObject.SetActive(true);
        }
    }
    public void UpComputer()
    {
        if (money >= CostClick[1] && activePref[1] < compPref.Length - 2)
        {
            upgradeClick.Play();

            money -= CostClick[1];
            CostClick[1] *= 2;
            money_click += 5;
            passive_click += 10;
            CostText[1].text = CostClick[1] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            activePref[1]++;
            compPref[activePref[1]].gameObject.SetActive(false);
            compPref[activePref[1] + 1].gameObject.SetActive(true);
        }
        else if (activePref[1] == lampPref.Length - 2)
        {
            upgradeClick.Play();
            activePref[1]++;
            compPref[activePref[1]].gameObject.SetActive(false);
            CostText[1].gameObject.SetActive(false);
            soldText[1].gameObject.SetActive(true);
        }
    }
    public void UpMonitor()
    {
        if (money >= CostClick[2] && activePref[2] < lampPref.Length - 2)
        {
            upgradeClick.Play();

            money -= CostClick[2];
            CostClick[2] *= 2;
            money_click += 10;
            passive_click += 5;
            CostText[2].text = CostClick[2] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            activePref[2]++;
            lampPref[activePref[2]].gameObject.SetActive(false);
            lampPref[activePref[2] + 1].gameObject.SetActive(true);
        }
        else if (activePref[1] == lampPref.Length - 2)
        {
            upgradeClick.Play();
            activePref[2]++;
            lampPref[activePref[2]].gameObject.SetActive(false);
            CostText[2].gameObject.SetActive(false);
            soldText[0].gameObject.SetActive(true);
        }
    }
    public void ToMain()
    {
        SaveCostShop();
        StartCoroutine(AudioButtons());
    }

    IEnumerator AudioButtons()
    {
        buttonsClick.Play();
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(0);

    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveCostShop();
        }
    }
#else
    private void OnApplicationQuit()
    {
        SaveCostShop();
    }
#endif
    private void SaveCostShop()
    {
        sv.CostClick = CostClick;
        sv.activePref = activePref;
        for (int i = 0; i < 6 ; i++)
        {
            sv.CostClick[i] = CostClick[i];
            sv.activePref[i] = activePref[i];
        }
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }

  

}

[Serializable]
public class Save
{
    public int[] CostClick;
    public int[] activePref;
    public Text CostText;
}
