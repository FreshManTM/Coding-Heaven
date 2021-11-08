using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]int money, money_click = 10, passive_click;
    public Text moneyText;

    [SerializeField]int[] activePref;

    [Header("Audio")]
    [SerializeField] AudioSource farmClick;
    [SerializeField] AudioSource buttonsClick;

    private Save sv;

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
        if (money_click == 0) money_click = 2;

        StartCoroutine(PassiveFarm());

        lampPref[activePref[0]].gameObject.SetActive(true);
        compPref[activePref[1]].gameObject.SetActive(true);
        monitorPref[activePref[2]].gameObject.SetActive(true);
        tablePref[activePref[3]].gameObject.SetActive(true);
        chairPref[activePref[4]].gameObject.SetActive(true);
        posterPref[activePref[5]].gameObject.SetActive(true);
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));

            for (int i = 0; i < 6; i++)
            {              
                activePref[i] = sv.activePref[i];
            }
        }
    }

    private void Update()
    {
        moneyText.text = "Money: " + money.ToString() + "$";       
    }

    public void ButtonClick()
    {
        money += money_click;
        EffectController.Instance.CreateClickEffect(money_click);
        Savings();
        farmClick.Play();
    }

    private void Savings()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("money_click", money_click);
        PlayerPrefs.SetInt("passive_click", passive_click);
    }

    IEnumerator PassiveFarm()
    {
        yield return new WaitForSeconds(1);
        money += passive_click;
        PlayerPrefs.SetInt("money", money);
        if(passive_click != 0)
            EffectController.Instance.CreatePassiveEffect(passive_click);
        StartCoroutine(PassiveFarm());
        
    }

    public void ToShop()
    {
        StartCoroutine(AudioButtons());
    }
    IEnumerator AudioButtons()
    {
        buttonsClick.Play();
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(1);

    }
    public void Buttonnnnnn()
    {
        money_click += 10;
        Savings();
    }
}
