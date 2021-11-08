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
    [SerializeField] int[] CostClick;
    public Text[] CostText;
    [SerializeField] GameObject[] soldText;
    [SerializeField] GameObject[] buttonsOff;

    [Header("Programing Upgrade")]
    [SerializeField] int progressScale = 10;
    [SerializeField] int progressProgram = 0;
    [SerializeField] Text progressText;
    [SerializeField] Text plusText;
    [SerializeField] int maxProgress = 5;
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

        progressScale = PlayerPrefs.GetInt("progressScale");
        progressProgram = PlayerPrefs.GetInt("progressProgram");
        maxProgress = PlayerPrefs.GetInt("maxProgress");

        PrefStart();
    }

    private void PrefStart()
    {
        if (activePref[0] < lampPref.Length )
        {
            lampPref[activePref[0]].gameObject.SetActive(true);
            soldText[0].gameObject.SetActive(false);

        }
        else
        {
            buttonsOff[0].gameObject.SetActive(false);
            soldText[0].gameObject.SetActive(true);
        }

        if (activePref[1] < compPref.Length )
        {
            compPref[activePref[1]].gameObject.SetActive(true);
            soldText[1].gameObject.SetActive(false);

        }
        else
        {
            buttonsOff[1].gameObject.SetActive(false);
            soldText[1].gameObject.SetActive(true);
        }

        if (activePref[2] < monitorPref.Length )
        {
            monitorPref[activePref[2]].gameObject.SetActive(true);
            soldText[2].gameObject.SetActive(false);

        }
        else
        {
            buttonsOff[2].gameObject.SetActive(false);
            soldText[2].gameObject.SetActive(true);
        }

        if (activePref[3] < tablePref.Length )
        {
            tablePref[activePref[3]].gameObject.SetActive(true);
            soldText[3].gameObject.SetActive(false);

        }
        else
        {
            buttonsOff[3].gameObject.SetActive(false);
            soldText[3].gameObject.SetActive(true);
        }

        if (activePref[4] < chairPref.Length )
        {
            chairPref[activePref[4]].gameObject.SetActive(true);
            soldText[4].gameObject.SetActive(false);

        }
        else
        {
            buttonsOff[4].gameObject.SetActive(false);
            soldText[4].gameObject.SetActive(true);
        }

        if (activePref[5] != 1)
        {
            posterPref[activePref[5]].gameObject.SetActive(true);
            soldText[5].gameObject.SetActive(false);

        }
        else
        {
            buttonsOff[5].gameObject.SetActive(false);
            soldText[5].gameObject.SetActive(true);
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
            CostClick[6] = sv.CostClick[6];
            CostText[6].text = sv.CostClick[6] + "$";
        }
    }

    void Update()
    {
        moneyText.text = "Money: " + money.ToString() + "$";
        SaveCostShop();
        progressText.text = progressProgram.ToString() + "/" + maxProgress.ToString();
        plusText.text = "Earnings for click: +" + progressScale.ToString() + "$" +'\n' + "Passive earnings: +" + progressScale.ToString() + "$";
        PlayerPrefs.SetInt("progressProgram", progressProgram);
        PlayerPrefs.SetInt("maxProgress", maxProgress);
    }

    public void UpdateProgress()
    {
        if (money >= CostClick[6] && progressProgram < maxProgress)
        {
            upgradeClick.Play();



            money -= CostClick[6];
            CostClick[6] += 500;
            money_click += progressScale;
            passive_click += progressScale - 5;
            CostText[6].text = CostClick[6] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);
            PlayerPrefs.SetInt("progressScale", progressScale);

            progressProgram++;
            PlayerPrefs.SetInt("progressProgram", progressProgram);
        }
        if (progressProgram == maxProgress)
        {
            progressProgram = 0;
            maxProgress += 5;
            progressScale += 10;
            PlayerPrefs.SetInt("progressScale", progressScale);
            PlayerPrefs.SetInt("progressProgram", progressProgram);
            PlayerPrefs.SetInt("maxProgress", maxProgress);
        }
    }
    public void UpLamp()
    {
        if(money >= CostClick[0] && activePref[0] < lampPref.Length - 1)
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

            lampPref[activePref[0]].gameObject.SetActive(false);
            lampPref[activePref[0] + 1].gameObject.SetActive(true);
            activePref[0]++;

        }
        else if(money >= CostClick[0] && activePref[0] == lampPref.Length - 1)
        {
            upgradeClick.Play();
            activePref[0]++;
            buttonsOff[0].gameObject.SetActive(false);
            soldText[0].gameObject.SetActive(true);
        }
    }
    public void UpComputer()
    {
        if (money >= CostClick[1] && activePref[1] < compPref.Length - 1)
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

            compPref[activePref[1]].gameObject.SetActive(false);
            compPref[activePref[1] + 1].gameObject.SetActive(true);
            activePref[1]++;

        }
        else if (money >= CostClick[1] && activePref[1] == compPref.Length - 1)
        {
            upgradeClick.Play();
            activePref[1]++;
            buttonsOff[1].gameObject.SetActive(false);
            soldText[1].gameObject.SetActive(true);
        }
    }
    public void UpMonitor()
    {
        if (money >= CostClick[2] && activePref[2] < monitorPref.Length - 1)
        {
            upgradeClick.Play();

            money -= CostClick[2];
            CostClick[2] *= 2;
            money_click += 10;
            passive_click += 10;
            CostText[2].text = CostClick[2] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            monitorPref[activePref[2]].gameObject.SetActive(false);
            monitorPref[activePref[2] + 1].gameObject.SetActive(true);
            activePref[2]++;

        }
        else if (money >= CostClick[2] && activePref[2] == monitorPref.Length - 1)
        {
            upgradeClick.Play();
            activePref[2]++;
            buttonsOff[2].gameObject.SetActive(false);
            soldText[2].gameObject.SetActive(true);
        }
    }
    public void UpTable()
    {
        if (money >= CostClick[3] && activePref[3] < tablePref.Length - 1)
        {
            upgradeClick.Play();

            money -= CostClick[3];
            CostClick[3] *= 2;
            money_click += 5;
            passive_click += 10;
            CostText[3].text = CostClick[3] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            tablePref[activePref[3]].gameObject.SetActive(false);
            tablePref[activePref[3] + 1].gameObject.SetActive(true);
            activePref[3]++;

        }
        else if (money >= CostClick[3] && activePref[3] == tablePref.Length - 1)
        {
            upgradeClick.Play();
            activePref[3]++;
            buttonsOff[3].gameObject.SetActive(false);
            soldText[3].gameObject.SetActive(true);
        }
    }
    public void UpChair()
    {
        if (money >= CostClick[4] && activePref[4] < chairPref.Length - 1)
        {
            upgradeClick.Play();

            money -= CostClick[4];
            CostClick[4] *= 2;
            money_click += 5;
            passive_click += 10;
            CostText[4].text = CostClick[4] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            chairPref[activePref[4]].gameObject.SetActive(false);
            chairPref[activePref[4] + 1].gameObject.SetActive(true);
            activePref[4]++;

        }
        else if (money >= CostClick[4] && activePref[4] == chairPref.Length - 1)
        {
            upgradeClick.Play();
            activePref[4]++;
            buttonsOff[4].gameObject.SetActive(false);
            soldText[4].gameObject.SetActive(true);
        }
    }
    public void UpPoster()
    {
        if (money >= CostClick[5] && activePref[5] < posterPref.Length - 1)
        {
            upgradeClick.Play();

            money -= CostClick[5];
            CostClick[5] *= 2;
            money_click += 5;
            passive_click += 10;
            CostText[5].text = CostClick[5] + "$";
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("money_click", money_click);
            PlayerPrefs.SetInt("passive_click", passive_click);

            activePref[5]++;

        }
        else if (money >= CostClick[5] && activePref[5] == posterPref.Length - 1)
        {
            upgradeClick.Play();
            activePref[5]++;
            buttonsOff[5].gameObject.SetActive(false);
            soldText[5].gameObject.SetActive(true);
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
        sv.CostClick[6] = CostClick[6];
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
