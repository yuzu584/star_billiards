using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Illogic.Data;

// ゲーム内の翻訳に関する処理
public class Localize : Singleton<Localize>
{
    // 言語ごとの情報が入った ScriptableObject
    [SerializeField] private LanguageData languageData;

    // 各言語のテキストが入った ScriptableObject
    [SerializeField] private ConfigContentStringData configContentSD;
    [SerializeField] private ConfigTopStringData configTopSD;
    [SerializeField] private MessageStringData messageSD;
    [SerializeField] private MissionStringData missionSD;
    [SerializeField] private PlayerStringData playerSD;
    [SerializeField] private ScreenStringData screenSD;
    [SerializeField] private SkillDetailsStringData skillDetailsSD;
    [SerializeField] private SkillNameStringData skillNameSD;
    [SerializeField] private SkillParameterStringData skillParameterSD;
    [SerializeField] private StageNameStringData stageNameSD;
    [SerializeField] private StarNameStringData starNameSD;
    [SerializeField] private KeyGuideStringData keyGuideSD;
    [SerializeField] private SystemStringData systemSD;

    private LanguageType language;
    public LanguageType Language
    {
        get { return language; }
        set
        {
            language = value;
            switchLanguageDele?.Invoke();
        }
    }

    public event System.Action switchLanguageDele;

    private void Start()
    {
        // 初期言語は日本語
        Language = LanguageType.Japanese;
    }

    // 文字列を取得
    public string GetString(StringGroup group, StringEnumStruct type)
    {
        string s;

        switch (group)
        {
            case StringGroup.ConfigContent:     s = GetString_ConfigContent(type.configContent);        break;
            case StringGroup.ConfigTop:         s = GetString_ConfigTop(type.configTop);                break;
            case StringGroup.Message:           s = GetString_Message(type.message);                    break;
            case StringGroup.Mission:           s = GetString_Mission(type.mission);                    break;
            case StringGroup.Player:            s = GetString_Player(type.player);                      break;
            case StringGroup.Screen:            s = GetString_Screen(type.screen);                      break;
            case StringGroup.SkillDetails:      s = GetString_SkillDetails(type.skillDetails);          break;
            case StringGroup.SkillName:         s = GetString_SkillName(type.skillName);                break;
            case StringGroup.SkillParameter:    s = GetString_SkillParameter(type.skillParameter);      break;
            case StringGroup.StageName:         s = GetString_StageName(type.stageName);                break;
            case StringGroup.StarName:          s = GetString_StarName(type.starName);                  break;
            case StringGroup.KeyGuide:          s = GetString_KeyGuide(type.keyGuide);                  break;
            case StringGroup.System:            s = GetString_System(type.system);                      break;
            default:                            s = "null";                                             break;
        }

        return s;
    }

    // 言語ごとのフォントを取得
    public Font GetFont()
    {
        // 言語ごとのフォントの数繰り返す
        for(int i = 0; i < languageData.fonts.Length; i++)
        {
            // 現在の言語が見つかったら
            if (languageData.fonts[i].language == Language)
            {
                // 現在の言語のフォントを返す
                return languageData.fonts[i].font;
            }
        }

        return null;
    }

    public string GetString_ConfigContent(AppParams.ParamsKey type)
    {
        for (int i = 0; i < configContentSD.strings.Length; i++)
            if (configContentSD.strings[i].type == type)
                return configContentSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_ConfigTop(EnumConfigTop type)
    {
        for (int i = 0; i < configTopSD.strings.Length; i++)
            if (configTopSD.strings[i].type == type)
                return configTopSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_Message(EnumMessage type)
    {
        for (int i = 0; i < messageSD.strings.Length; i++)
            if (messageSD.strings[i].type == type)
                return messageSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_Mission(EnumMission type)
    {
        for (int i = 0; i < missionSD.strings.Length; i++)
            if (missionSD.strings[i].type == type)
                return missionSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_Player(EnumPlayer type)
    {
        for (int i = 0; i < playerSD.strings.Length; i++)
            if (playerSD.strings[i].type == type)
                return playerSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_Screen(EnumScreen type)
    {
        for (int i = 0; i < screenSD.strings.Length; i++)
            if (screenSD.strings[i].type == type)
                return screenSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_SkillDetails(EnumSkillDetails type)
    {
        for (int i = 0; i < skillDetailsSD.strings.Length; i++)
            if (skillDetailsSD.strings[i].type == type)
                return skillDetailsSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_SkillName(EnumSkillName type)
    {
        for (int i = 0; i < skillNameSD.strings.Length; i++)
            if (skillNameSD.strings[i].type == type)
                return skillNameSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_SkillParameter(EnumSkillParameter type)
    {
        for (int i = 0; i < skillParameterSD.strings.Length; i++)
            if (skillParameterSD.strings[i].type == type)
                return skillParameterSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_StageName(EnumStageName type)
    {
        for (int i = 0; i < stageNameSD.strings.Length; i++)
            if (stageNameSD.strings[i].type == type)
                return stageNameSD.strings[i].text[(int)Language];

        return "null";
    }


    public string GetString_StarName(EnumStarName type)
    {
        for (int i = 0; i < starNameSD.strings.Length; i++)
            if (starNameSD.strings[i].type == type)
                return starNameSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_KeyGuide(EnumKeyGuide type)
    {
        for (int i = 0; i < keyGuideSD.strings.Length; i++)
            if (keyGuideSD.strings[i].type == type)
                return keyGuideSD.strings[i].text[(int)Language];

        return "null";
    }

    public string GetString_System(EnumSystem type)
    {
        for (int i = 0; i < systemSD.strings.Length; i++)
            if (systemSD.strings[i].type == type)
                return systemSD.strings[i].text[(int)Language];

        return "null";
    }
}
