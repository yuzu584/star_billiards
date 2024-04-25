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

        // ���`��ԂŎg�p����N���X�z��Ƃ��̒������擾
        var lerpsProperty = serializedObject.FindProperty("lerpStructs");
        int lerpsCount = lerpsProperty.arraySize;

        // ���`��ԂŎg�p����N���X�z��̒����� Inspector ����ݒ�ł���悤��
        lerpsCount = EditorGUILayout.IntField("�v�f��", lerpsCount);
        lerpsProperty.arraySize = lerpsCount;

        // �z����R�s�[���ĕۑ�
        savedFoldOut = foldOut;

        // ������ύX
        foldOut = new bool[lerpsCount];

        // �ۑ������z��̒��g����
        for (int i = 0; i < foldOut.Length; ++i)
        {
            if (savedFoldOut == null) break;
            if (i >= savedFoldOut.Length) break;
            foldOut[i] = savedFoldOut[i];
        }

        // ���`��ԂŎg�p����N���X�z��̒������J��Ԃ�
        for (int i = 0; i < lerpsCount; ++i)
        {
            // ���`��ԂŎg�p����N���X���擾
            var lerp = lerpsProperty.GetArrayElementAtIndex(i);
            var ui = lerp.FindPropertyRelative("ui");
            UnityNullCheck nullChecker = new UnityNullCheck();
            if (nullChecker.IsNull(ui.objectReferenceValue))
            {
                // ���`��Ԃ��g�p���� UI �����݂��Ȃ���Ό��o���̕������ null
                foldOut[i] = EditorGUILayout.Foldout(foldOut[i], "null");
            }
            else
            {
                // ���o���̕�����͐��`��Ԃ��g�p���� UI �̖��O�Ƃ��̌^��
                // �^���� "UnityEngine.UI." ���܂܂�Ă������菜��
                string s = $"{ui.objectReferenceValue.name}({ui.objectReferenceValue.GetType().ToString().Replace("UnityEngine.UI.", "")})";

                foldOut[i] = EditorGUILayout.Foldout(foldOut[i], s);
            }

            if (!foldOut[i]) continue;

            // Inspector �ɕ`��
            Drawlerp(lerp);
        }

        serializedObject.ApplyModifiedProperties();
    }

    // ���`��Ԃ��s��UI�E�F�E���W�E�X�P�[���̍\���̗̂v�f��`��
    void Drawlerp(SerializedProperty lerp)
    {
        // �����󂯂�
        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        var ui = lerp.FindPropertyRelative("ui");
        GUILayout.BeginHorizontal();
        GUILayout.Label("���`��Ԃ��s��UI");
        ui.objectReferenceValue = EditorGUILayout.ObjectField(ui.objectReferenceValue, typeof(UIBehaviour), true);
        GUILayout.EndHorizontal();

        DrawColorStr(lerp);
        DrawPositionStr(lerp);
        DrawScaleStr(lerp);
    }

    // �F�̍\���̗̂v�f��`��
    void DrawColorStr(SerializedProperty lerp)
    {
        // �K�v�ȍ\���̂��擾
        var lerpColor = lerp.FindPropertyRelative("color");
        var use = lerpColor.FindPropertyRelative("use");
        var changedColor = lerpColor.FindPropertyRelative("changedColor");
        var fadetime = lerpColor.FindPropertyRelative("fadeTime");

        // ���x���̕`��
        GUILayout.BeginHorizontal();
        GUILayout.Label("�F�̐��`��Ԃ��g�p���邩");
        // ���`��ԂŎg�p����Ȃ�\��
        if (use.boolValue)
        {
            GUILayout.Label("�ω���̐F");
            GUILayout.Label("�t�F�[�h����");
        }
        GUILayout.EndHorizontal();

        // �ϐ��̕`��
        GUILayout.BeginHorizontal();
        use.boolValue = EditorGUILayout.Toggle(use.boolValue);
        // ���`��ԂŎg�p����Ȃ�\��
        if (use.boolValue)
        {
            changedColor.colorValue = EditorGUILayout.ColorField(changedColor.colorValue);
            fadetime.floatValue = EditorGUILayout.FloatField(fadetime.floatValue);
        }
        GUILayout.EndHorizontal();
    }

    // ���W�̍\���̗̂v�f��`��
    void DrawPositionStr(SerializedProperty lerp)
    {
        // �K�v�ȍ\���̂��擾
        var lerpPosition = lerp.FindPropertyRelative("position");
        var use = lerpPosition.FindPropertyRelative("use");
        var posFluctuation = lerpPosition.FindPropertyRelative("posFluctuation");
        var fadetime = lerpPosition.FindPropertyRelative("fadeTime");

        // ���x���̕`��
        GUILayout.BeginHorizontal();
        GUILayout.Label("���W�̐��`��Ԃ��g�p���邩");
        // ���`��ԂŎg�p����Ȃ�\��
        if (use.boolValue)
        {
            GUILayout.Label("���W�ω���");
            GUILayout.Label("�t�F�[�h����");
        }
        GUILayout.EndHorizontal();

        // �ϐ��̕`��
        GUILayout.BeginHorizontal();
        use.boolValue = EditorGUILayout.Toggle(use.boolValue);
        // ���`��ԂŎg�p����Ȃ�\��
        if (use.boolValue)
        {
            posFluctuation.vector3Value = EditorGUILayout.Vector3Field("", posFluctuation.vector3Value);
            fadetime.floatValue = EditorGUILayout.FloatField(fadetime.floatValue);
        }
        GUILayout.EndHorizontal();
    }

    // �X�P�[���̍\���̗̂v�f��`��
    void DrawScaleStr(SerializedProperty lerp)
    {
        // �K�v�ȍ\���̂��擾
        var lerpPosition = lerp.FindPropertyRelative("scale");
        var use = lerpPosition.FindPropertyRelative("use");
        var scaleFluctuation = lerpPosition.FindPropertyRelative("scaleFluctuation");
        var fadetime = lerpPosition.FindPropertyRelative("fadeTime");

        // ���x���̕`��
        GUILayout.BeginHorizontal();
        GUILayout.Label("�X�P�[���̐��`��Ԃ��g�p���邩");
        // ���`��ԂŎg�p����Ȃ�\��
        if (use.boolValue)
        {
            GUILayout.Label("�X�P�[���ω���");
            GUILayout.Label("�t�F�[�h����");
        }
        GUILayout.EndHorizontal();

        // �ϐ��̕`��
        GUILayout.BeginHorizontal();
        use.boolValue = EditorGUILayout.Toggle(use.boolValue);
        // ���`��ԂŎg�p����Ȃ�\��
        if (use.boolValue)
        {
            scaleFluctuation.vector2Value = EditorGUILayout.Vector2Field("", scaleFluctuation.vector2Value);
            fadetime.floatValue = EditorGUILayout.FloatField(fadetime.floatValue);
        }
        GUILayout.EndHorizontal();
    }
}
#endif