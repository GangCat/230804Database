using System.Collections.Generic;

delegate void VoidvoidDelegate();
delegate int IntvoidDelegate();
delegate int IntIntDelegate(int _value);

public delegate void voidListPoolingObjectDelegate(List<IPoolingObject> _enemyList);

public delegate void AttackDelegate(int _dmg = 1);
// �̷��Ե� �ȴ�
// public ������Ѵ�.