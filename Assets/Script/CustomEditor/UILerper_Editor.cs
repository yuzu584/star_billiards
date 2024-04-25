#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using StarBilliards.System;

[CustomEditor(typeof(UILerper))]
public class UILerper_Editor : Editor
{
    private UILerper _uiLerper;

    private bool[] foldOut;
    private bool[] savedFoldOut;

    private void OnEnable()
    {
        _uiLerper = target as UILerper;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        // 線形補間で使用するクラス配列とその長さを取得
        var lerpsProperty = serializedObject.FindProperty("lerpStructs");
        int lerpsCount = lerpsProperty.arraySize;

        // 線形補間で使用するクラス配列の長さを Inspector から設定できるように
        lerpsCount = EditorGUILayout.IntField("要素数", lerpsCount);
        lerpsProperty.arraySize = lerpsCount;

        // 配列をコピーして保存
        savedFoldOut = foldOut;

        // 長さを変更
        foldOut = new bool[lerpsCount];

        // 保存した配列の中身を代入
        for (int i = 0; i < foldOut.Length; ++i)
        {
            if (savedFoldOut == null) break;
            if (i >= savedFoldOut.Length) break;
            foldOut[i] = savedFoldOut[i];
        }

        // 線形補間で使用するクラス配列の長さ分繰り返す
        for (int i = 0; i < lerpsCount; ++i)
        {
            // 線形補間で使用するクラスを取得
            var lerp = lerpsProperty.GetArrayElementAtIndex(i);
            var ui = lerp.FindPropertyRelative("ui");
            UnityNullCheck nullChecker = new UnityNullCheck();
            if (nullChecker.IsNull(ui.objectReferenceValue))
            {
                // 線形補間を使用する UI が存在しなければ見出しの文字列は null
                foldOut[i] = EditorGUILayout.Foldout(foldOut[i], "null");
            }
            else
            {
                // 見出しの文字列は線形補間を使用する UI の名前とその型名
                // 型名に "UnityEngine.UI." が含まれていたら取り除く
                string s = $"{ui.objectReferenceValue.name}({ui.objectReferenceValue.GetType().ToString().Replace("UnityEngine.UI.", "")})";

                foldOut[i] = EditorGUILayout.Foldout(foldOut[i], s);
            }

            if (!foldOut[i]) continue;

            // Inspector に描画
            Drawlerp(lerp);
        }

        serializedObject.ApplyModifiedProperties();
    }

    // 線形補間を行うUI・色・座標・スケールの構造体の要素を描画
    void Drawlerp(SerializedProperty lerp)
    {
        // 少し空ける
        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        var ui = lerp.FindPropertyRelative("ui");
        GUILayout.BeginHorizontal();
        GUILayout.Label("線形補間を行うUI");
        ui.objectReferenceValue = EditorGUILayout.ObjectField(ui.objectReferenceValue, typeof(UIBehaviour), true);
        GUILayout.EndHorizontal();

        DrawColorStr(lerp);
        DrawPositionStr(lerp);
        DrawScaleStr(lerp);
    }

    // 色の構造体の要素を描画
    void DrawColorStr(SerializedProperty lerp)
    {
        // 必要な構造体を取得
        var lerpColor = lerp.FindPropertyRelative("color");
        var use = lerpColor.FindPropertyRelative("use");
        var changedColor = lerpColor.FindPropertyRelative("changedColor");
        var fadetime = lerpColor.FindPropertyRelative("fadeTime");

        // ラベルの描画
        GUILayout.BeginHorizontal();
        GUILayout.Label("色の線形補間を使用するか");
        // 線形補間で使用するなら表示
        if (use.boolValue)
        {
            GUILayout.Label("変化後の色");
            GUILayout.Label("フェード時間");
        }
        GUILayout.EndHorizontal();

        // 変数の描画
        GUILayout.BeginHorizontal();
        use.boolValue = EditorGUILayout.Toggle(use.boolValue);
        // 線形補間で使用するなら表示
        if (use.boolValue)
        {
            changedColor.colorValue = EditorGUILayout.ColorField(changedColor.colorValue);
            fadetime.floatValue = EditorGUILayout.FloatField(fadetime.floatValue);
        }
        GUILayout.EndHorizontal();
    }

    // 座標の構造体の要素を描画
    void DrawPositionStr(SerializedProperty lerp)
    {
        // 必要な構造体を取得
        var lerpPosition = lerp.FindPropertyRelative("position");
        var use = lerpPosition.FindPropertyRelative("use");
        var posFluctuation = lerpPosition.FindPropertyRelative("posFluctuation");
        var fadetime = lerpPosition.FindPropertyRelative("fadeTime");

        // ラベルの描画
        GUILayout.BeginHorizontal();
        GUILayout.Label("座標の線形補間を使用するか");
        // 線形補間で使用するなら表示
        if (use.boolValue)
        {
            GUILayout.Label("座標変化量");
            GUILayout.Label("フェード時間");
        }
        GUILayout.EndHorizontal();

        // 変数の描画
        GUILayout.BeginHorizontal();
        use.boolValue = EditorGUILayout.Toggle(use.boolValue);
        // 線形補間で使用するなら表示
        if (use.boolValue)
        {
            posFluctuation.vector3Value = EditorGUILayout.Vector3Field("", posFluctuation.vector3Value);
            fadetime.floatValue = EditorGUILayout.FloatField(fadetime.floatValue);
        }
        GUILayout.EndHorizontal();
    }

    // スケールの構造体の要素を描画
    void DrawScaleStr(SerializedProperty lerp)
    {
        // 必要な構造体を取得
        var lerpPosition = lerp.FindPropertyRelative("scale");
        var use = lerpPosition.FindPropertyRelative("use");
        var scaleFluctuation = lerpPosition.FindPropertyRelative("scaleFluctuation");
        var fadetime = lerpPosition.FindPropertyRelative("fadeTime");

        // ラベルの描画
        GUILayout.BeginHorizontal();
        GUILayout.Label("スケールの線形補間を使用するか");
        // 線形補間で使用するなら表示
        if (use.boolValue)
        {
            GUILayout.Label("スケール変化量");
            GUILayout.Label("フェード時間");
        }
        GUILayout.EndHorizontal();

        // 変数の描画
        GUILayout.BeginHorizontal();
        use.boolValue = EditorGUILayout.Toggle(use.boolValue);
        // 線形補間で使用するなら表示
        if (use.boolValue)
        {
            scaleFluctuation.vector2Value = EditorGUILayout.Vector2Field("", scaleFluctuation.vector2Value);
            fadetime.floatValue = EditorGUILayout.FloatField(fadetime.floatValue);
        }
        GUILayout.EndHorizontal();
    }
}
#endif