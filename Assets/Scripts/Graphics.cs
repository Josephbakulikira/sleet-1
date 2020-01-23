using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    [SerializeField]
    private GameObject dot;
    [SerializeField]
    private int dotTotal;
    [SerializeField]
    private AnimationCurve followCurve;
    [SerializeField]
    private float followSpeed;
    [SerializeField]
    private AnimationCurve expandCurve;
    [SerializeField]
    private float expandAmount;
    [SerializeField]
    private float expandSpeed;

    private float m_scrollAmount;
    private float m_dotGap;
    private GameObject[] m_dotArray;
    private Vector3 m_startSize;
    private Vector3 m_targetSize;

    private void Start()
    {
        m_dotGap = 1f / dotTotal; //percentage of one dots reative to total
        InitPulseEffectVariables();
        SpawnDots();
    }
    void InitPulseEffectVariables()
    {
        m_startSize = transform.localScale;
        m_targetSize = m_startSize * expandAmount;

    }
    void SpawnDots()
    {
        m_dotArray = new GameObject[dotTotal];

        for (int i = 0; i < dotTotal; i++)
        {
            GameObject point = Instantiate(dot);
            dot.SetActive(false);
            m_dotArray[i] = point;
        }     
    }

    public void SetDotPosition(Vector3 startPosition, Vector3 endPosition)
    {
        for (int i = 0; i < dotTotal; i++)
        {
            Vector3 _dotPos = m_dotArray[i].transform.position;
            Vector3 _targetPos = Vector2.Lerp(startPosition, endPosition, i * m_dotGap);

            float _smoothSpeed = (1f- followCurve.Evaluate(i * m_dotGap)) * followSpeed;

            //m_dotArray[i].transform.position = _targetPos;
            m_dotArray[i].transform.position = Vector2.Lerp(_dotPos, _targetPos, _smoothSpeed * Time.deltaTime);
        }
    }

    public void DotSwitcher(bool state)
    {
        for (int i = 0; i < dotTotal; i++)
        {
            m_dotArray[i].SetActive(state);
        }
    }

    public void setDotStartPos(Vector3 pos)
    {
        for (int i = 0; i < dotTotal; i++)
        {
            m_dotArray[i].transform.position = pos;
        }
    }
    public void MakePlayerPulse()
    {
        m_scrollAmount += Time.deltaTime * expandSpeed;

        float _percent = expandCurve.Evaluate(m_scrollAmount);

        transform.localScale = Vector2.Lerp(m_startSize, m_targetSize, _percent);
    }
    public void ResetPlayerSize()
    {
        transform.localScale = m_startSize;
        m_scrollAmount = 0f;
    }
}
