using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterManager : MonoBehaviour
{
    [SerializeField] private GameObject[] waters;
    [SerializeField] private float endValue = 0f;
    [SerializeField] private float duration = 0f;

    private void Start()
    {
        Invoke("StartWater", 1800);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Alpha9) || Input.GetKey(KeyCode.Alpha0))
        {
            StartWater();
        }
    }

    private void StartWater()
    {
        for (int i = 0; i < waters.Length; i++)
        {
            waters[i].transform.DOScaleY(endValue, duration).OnComplete(() =>
            {
                GameManager.Instance.GameOver();
            });
        }
    }
}
