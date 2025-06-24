[System.Serializable]
public class MonsterData
{
    public int id;
    public string name;
    public int hp;
    public int exp;
    public float speed;
    public int damage;
    public string skillType1;
    public string skillType2;
}

[System.Serializable]
public class WaveData
{
    public int id;
    public int waveType;
    public int waveCount;
    public float waveTerm;
    public int mob0Count;
    public int mob1Count;
    public int mob2Count;
}