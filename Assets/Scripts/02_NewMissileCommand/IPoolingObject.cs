using UnityEngine;

public interface IPoolingObject
{
    // �����ǿ����� ������� ���̱� ������ ����Ʈ�� public�̴�.
    void Init(GameObject _target);
    void Release();
}
