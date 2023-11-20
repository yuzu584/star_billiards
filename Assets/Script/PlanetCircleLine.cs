using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���̉~�̐����Ǘ�
public class PlanetCircleLine : MonoBehaviour
{
    public int segments = 20;    // �Z�O�����g��
    public float width = 0.1f;   // ��
    public GameObject centerObj; // ���S�̃I�u�W�F�N�g
    public GameObject radiusObj; // �O���̃I�u�W�F�N�g
    public GameObject parentObj; // �e�I�u�W�F�N�g

    void Start()
    {
        // Linerenderer�R���|�[�l���g���Z�b�g
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // �}�e���A����ݒ�
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // ���̑�����ݒ�
        lineRenderer.widthMultiplier = width;

        // �Z�O�����g����ݒ�
        lineRenderer.positionCount = segments + 1;

        float deltaTheta = (2f * Mathf.PI) / segments; // �Z�O�����g���Ƃ̊p�x
        float theta = 0f;                              // �V�[�^

        // ���S�ƊO���̃I�u�W�F�N�g�̍��W����
        Vector3 centerPos = centerObj.transform.position;
        Vector3 radiusPos = radiusObj.transform.position;

        // ���S�ƊO���̃I�u�W�F�N�g�̋����𔼌a�ɑ��
        float radius = Vector3.Distance(centerPos, radiusPos);

        // �Z�O�����g���Ƃ̐ݒ�
        for (int i = 0; i < segments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0f, z) + parentObj.transform.position;
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
