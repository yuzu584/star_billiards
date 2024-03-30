using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ポップアップの親クラス
public class PopupParent : MonoBehaviour
{
    [SerializeField] protected PopupManager.PopupType popupType;    // ポップアップの種類

    protected PopupManager popupMana;
    protected ScreenController scrCon;
    protected InputController input;
    protected Lerp lerp;
    protected Localize localize;

    protected int index = -1;                                       // PopupManager が管理するポップアップの配列の何番目のポップアップか
    [SerializeField] protected bool onChangeScreenDestroy = false;  // 画面遷移時にポップアップを削除するか
    [SerializeField] protected bool onChangeLootDestroy = false;    // 階層遷移時にポップアップを削除するか
    [SerializeField] protected Text popupText;                      // ポップアップのテキスト

    protected virtual void Start()
    {
        popupMana = PopupManager.instance;
        scrCon = ScreenController.instance;
        input = InputController.instance;
        localize = Localize.instance;

        // Lerp をアタッチ
        lerp ??= gameObject.AddComponent<Lerp>();

        // フォントを設定
        popupText.font = localize.GetFont();

        // 画面遷移時にポップアップを削除
        if (onChangeScreenDestroy)
            scrCon.changeScreen += Destroy;

        // 階層遷移時にポップアップを削除
        if (onChangeLootDestroy)
            scrCon.changeLoot += Destroy;
    }

    protected virtual void OnDestroy()
    {
        if (onChangeScreenDestroy)
            scrCon.changeScreen -= Destroy;

        if (onChangeLootDestroy)
            scrCon.changeLoot -= Destroy;
    }

    // ポップアップの処理
    public virtual IEnumerator Process(string text, Transform parentT, int num)
    {
        Debug.Log("ポップアップの処理が設定されていません");
        yield return null;
    }

    // ポップアップを削除
    public void Destroy()
    {
        // インスタンスが存在すれば削除
        if (popupMana.popupContent[(int)popupType].instance[index] != null)
            Destroy(popupMana.popupContent[(int)popupType].instance[index]);

        // インスタンスを格納する配列に null を代入
        popupMana.popupContent[(int)popupType].instance[index] = null;

        // 取得した PopupParent クラスを格納する配列に null を代入
        popupMana.popupContent[(int)popupType].component[index] = null;

        // コルーチンが存在すれば停止する
        if (popupMana.popupContent[(int)popupType].coroutines[index] != null)
            popupMana.StopCoroutineOfPopupContent(popupMana.popupContent[(int)popupType], index);

        // コルーチンを格納する配列に null を代入する
        popupMana.popupContent[(int)popupType].coroutines[index] = null;
    }
}
