using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarBilliards.System
{
    public class UnityNullCheck
    {
        // Unity�̋U�� null �p�� null ����֐�
        public bool IsNull(Object obj)
        {
            return obj == null;
        }
    }
}