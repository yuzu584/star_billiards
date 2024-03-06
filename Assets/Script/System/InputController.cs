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
    public Vector2 Game_Move;
    public Vector2 Game_Look;
    public float Game_Shot;
    public float Game_Aim;
    public float Game_UseSkill;
    public float Game_ChangeSkill;
    public float Game_Pause;
    public Vector2 UI_Move;
    public float UI_Positive;
    public float UI_Negative;

    void Update()
    {
        /*
        Game_Move = actions.Game.Move.ReadValue<Vector2>();
        Game_Look = actions.Game.Look.ReadValue<Vector2>();
        Game_Shot = actions.Game.Shot.ReadValue<float>();
        Game_Aim = actions.Game.Aim.ReadValue<float>();
        Game_UseSkill = actions.Game.UseSkill.ReadValue<float>();
        Game_ChangeSkill = actions.Game.ChangeSkill.ReadValue<float>();
        Game_Pause = actions.Game.Pause.ReadValue<float>();
        UI_Move = actions.UI.Move.ReadValue<Vector2>();
        UI_Positive = actions.UI.Positive.ReadValue<float>();
        UI_Negative = actions.UI.Negative.ReadValue<float>();*/
    }

    private void Awake()
    {
        // インスタンスを生成
        actions = new PlayerActions();
        actions.Game.UseSkill.performed += Game_OnUseSkill;
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
                Debug.Log("0");
                actions.Game.Enable();
                break;
            case 1:
                Debug.Log("1");
                actions.UI.Enable();
                break;
        }
    }

    public void Game_OnMove(InputAction.CallbackContext context)
    {
        if (context.started) return;

        Game_Move = context.ReadValue<Vector2>();
        Debug.Log("move");
    }

    public void Game_OnLook(InputAction.CallbackContext context)
    {
        if (context.started) return;

        Game_Look = context.ReadValue<Vector2>();
        Debug.Log("look");
    }

    public void Game_OnShot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Game_Shot = context.ReadValue<float>();
        Debug.Log("shot");
    }

    public void Game_OnAim(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Game_Aim = context.ReadValue<float>();
        Debug.Log("aim");
    }

    public void Game_OnUseSkill(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Game_UseSkill = context.ReadValue<float>();
        Debug.Log("useskill");
    }

    public void Game_OnChangeSkill(InputAction.CallbackContext context)
    {
        //if (!context.performed) return;

        Game_ChangeSkill = context.ReadValue<float>();
        Debug.Log("changeskill");
    }

    public void Game_OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Game_Pause = context.ReadValue<float>();
        Debug.Log("pause");
    }

    public void UI_OnMove(InputAction.CallbackContext context)
    {
        if (context.started) return;

        UI_Move = context.ReadValue<Vector2>();
        Debug.Log("move");
    }

    public void UI_OnPositive(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        UI_Positive = context.ReadValue<float>();
        Debug.Log("positive");
    }

    public void UI_OnNegative(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        UI_Negative = context.ReadValue<float>();
        Debug.Log("negative");
    }
}
