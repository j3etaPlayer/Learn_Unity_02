using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    // ������ �ʿ��� Ŭ������ iSaveManager�� ��ӽ��� �ѹ��� ������ �� �ִ�.
    // ���� �̸��� �Լ���...

    public void SaveData(ref GameData data);
    public void LoadData(GameData data);
}
