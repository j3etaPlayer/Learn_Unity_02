using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    // 저장이 필요한 클래스를 iSaveManager를 상속시켜 한번에 관리할 수 있다.
    // 같은 이름의 함수를...

    public void SaveData(ref GameData data);
    public void LoadData(GameData data);
}
