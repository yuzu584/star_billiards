using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����ŕύX�\�Ȓl���܂Ƃ߂����O���
namespace AppParam
{
    public static class Param_Player
    {
        public static FunctionalValue<int> energy = new FunctionalValue<int>(1000, 1000, 0);                        // �G�l���M�[
    }

    public static class Param_Camera
    {
        public static FunctionalValue<int> fov = new FunctionalValue<int>(60, 90, 60);                              // �J�����̎���p

        public static FunctionalValue<float> angleMoveSpeed = new FunctionalValue<float>(1.0f, 100.0f, 0.01f);      // �J�����̎��_�ړ����x
    }
}
