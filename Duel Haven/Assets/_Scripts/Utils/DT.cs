using TMPro;
using UnityEngine;

public class DT : MonoBehaviour
{
    public static DT I { get; private set; }

    [SerializeField] private TMP_Text debugTextA;
    [SerializeField] private TMP_Text debugTextB;
    [SerializeField] private bool enableDebug = true;

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
    }

    public void SetA(string msg)
    {
        if (enableDebug && debugTextA != null)
            debugTextA.text = msg;
    }

    public void SetB(string msg)
    {
        if (enableDebug && debugTextB != null)
            debugTextB.text = msg;
    }

    public void AppendA(string msg)
    {
        if (enableDebug && debugTextA != null)
            debugTextA.text += "\n" + msg;
    }

    public void AppendB(string msg)
    {
        if (enableDebug && debugTextB != null)
            debugTextB.text += "\n" + msg;
    }

    public void Clear()
    {
        if (debugTextA != null) debugTextA.text = "";
        if (debugTextB != null) debugTextB.text = "";
    }
}
