using UnityEngine;

public class EffectController : MonoBehaviour
{
    public static EffectController Instance;

    [SerializeField] private Effects effectPref;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateClickEffect(int value)
    {
        var pref = Instantiate(effectPref, transform, false);
        pref.SetPosition();
        pref.SetValue(value);
    }
    public void CreatePassiveEffect(int value)
    {
        var pref = Instantiate(effectPref, transform, false);
        pref.SetPosition();
        pref.SetValue(value);
        pref.SetPassiveColor();
    }


}
