using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// InputSystemの入力を管理
public class InputController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController;
    [SerializeField] private ScreenData screenData;

    private PlayerActions actions; // InputSystem

    // デリゲートを定義
    public delegate void Game_OnMoveDele(Vector2 mVec);
    public delegate void Game_OnLookDele(Vector2 vec);
    public delegate void Game_OnShotDele(float value);
    public delegate void Game_OnAimDele(float value);
    public delegate void Game_OnUseSkillDele(float value);
    public delegate void Game_OnChangeSkillDele(float value);
    public delegate void Game_OnPauseDele(float value);
    public delegate void UI_OnMoveDele(Vector2 mVec);
    public delegate void UI_OnPositiveDele(float value);
    public delegate void UI_OnNegativeDele(float value);

    // デリゲートを宣言
    public Game_OnMoveDele game_OnMoveDele;
    public Game_OnLookDele game_OnLookDele;
    public Game_OnShotDele game_OnShotDele;
    public Game_OnAimDele game_OnAimDele;
    public Game_OnUseSkillDele game_OnUseSkillDele;
    public Game_OnChangeSkillDele game_OnChangeSkillDele;
    public Game_OnPauseDele game_OnPauseDele;
    public UI_OnMoveDele ui_OnMoveDele;
    public UI_OnPositiveDele ui_OnPositiveDele;
    public UI_OnNegativeDele ui_OnNegativeDele;

    private void Awake()
    {
        // インスタンスを生成
        actions = new PlayerActions();

        // イベントを設定
        actions.Game.Move.performed += Game_OnMove;
        actions.Game.Look.performed += Game_OnLook;
        actions.Game.Shot.performed += Game_OnShot;
        actions.Game.Aim.performed += Game_OnAim;
        actions.Game.UseSkill.performed += Game_OnUseSkill;
        actions.Game.ChangeSkill.performed += Game_OnChangeSkill;
        actions.Game.Pause.performed += Game_OnPause;
        actions.UI.Move.performed += UI_OnMove;
        actions.UI.Positive.performed += UI_OnPositive;
        actions.UI.Negative.performed += UI_OnNegative;
    }

    void Start()
    {
        // 画面遷移時に入力状態の有効無効を設定する
        screenController.changeScreen += SetInputs;
    }

    // 入力状態の有効無効を設定
    public void SetInputs()
    {
        // 入力を無効化
        switch (screenData.screenList[screenController.oldScreenNum].inputType)
        {
            case 0:
                actions.Game.Disable();
                break;
            case 1:
                actions.UI.Disable();
                break;
        }

        // 入力を有効化
        switch (screenData.screenList[screenController.ScreenNum].inputType)
        {
            case 0:
                actions.Game.Enable();
                break;
            case 1:
                actions.UI.Enable();
                break;
        }
    }

    public void Game_OnMove(InputAction.CallbackContext context)
    {
        game_OnMoveDele(context.ReadValue<Vector2>());

        Debug.Log("move");
    }

    public void Game_OnLook(InputAction.CallbackContext context)
    {
        game_OnLookDele(context.ReadValue<Vector2>());

        Debug.Log("look");
    }

    public void Game_OnShot(InputAction.CallbackContext context)
    {
        game_OnShotDele(context.ReadValue<float>());

        Debug.Log("shot");
    }

    public void Game_OnAim(InputAction.CallbackContext context)
    {
        game_OnAimDele(context.ReadValue<float>());

        Debug.Log("aim");
    }

    public void Game_OnUseSkill(InputAction.CallbackContext context)
    {
        game_OnUseSkillDele(context.ReadValue<float>());

        Debug.Log("useskill");
    }

    public void Game_OnChangeSkill(InputAction.CallbackContext context)
    {
        game_OnChangeSkillDele(context.ReadValue<float>());

        Debug.Log("changeskill");
    }

    public void Game_OnPause(InputAction.CallbackContext context)
    {
        game_OnPauseDele(context.ReadValue<float>());

        Debug.Log("pause");
    }

    public void UI_OnMove(InputAction.CallbackContext context)
    {
        ui_OnMoveDele(context.ReadValue<Vector2>());

        Debug.Log("move");
    }

    public void UI_OnPositive(InputAction.CallbackContext context)
    {
        ui_OnPositiveDele(context.ReadValue<float>());

        Debug.Log("positive");
    }

    public void UI_OnNegative(InputAction.CallbackContext context)
    {
        ui_OnNegativeDele(context.ReadValue<float>());

        Debug.Log("negative");
    }
}
