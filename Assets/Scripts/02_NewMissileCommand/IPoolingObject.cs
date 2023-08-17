using UnityEngine;

public interface IPoolingObject
{
    // 재정의용으로 만들어진 것이기 때문에 디폴트가 public이다.
    void Init(GameObject _target);
    void Release();
}
