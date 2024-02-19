using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内の初期化処理を管理
public class Initialize : MonoBehaviour
{
    // ステージ開始時の初期化処理
    public delegate void Init_Stage();
    public Init_Stage init_Stage;
}
