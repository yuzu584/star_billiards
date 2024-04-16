using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キー操作のガイドのUIを管理
public class KeyGuideUI : MonoBehaviour
{
    [SerializeField] private GameObject parentObj;

    public List<KeyGuide> keyGuides = new List<KeyGuide>();
}
