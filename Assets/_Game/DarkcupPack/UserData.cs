using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KingdomStat
{
    public int people = 100;
    public int gold = 200;
    public int happiness = 100;

    //hiden stat
    public int dayCount;
    public int morningCount;
    public int nightCount;

    public int dayNight;
    public int isAngry;
    public int timeline;

    public int chiaSeThuocCount;
    public int batTromCount;
    public int tangThueCount;
    public int nangCapVuKhiCount;
    public int giupAnXin;

    public bool thuNhanHiepSiLangThang;
    public bool xayNhaTho;
    public bool dangXamLuocNuocKhac;
    public bool dangBiXamLuoc;

    public int lastPeople;
    public int lastHappiness;
    public int lastGold;

    public Dictionary<string,bool> happens;
    public Dictionary<string,bool> selectYes;
    public Dictionary<string,bool> selectNo;
    public Queue<string> happenedQueue;
    public List<string> specialBuildings;
    public List<int> randomHouseIndexs;

    public KingdomStat()
    {
        happens = new Dictionary<string, bool>();
        selectYes = new Dictionary<string, bool>();
        selectNo = new Dictionary<string, bool>();
        happenedQueue = new Queue<string>();
        specialBuildings = new List<string>();
        lastGold = gold;
        lastPeople = people;
        lastHappiness = happiness;
    }
}

[System.Serializable]
public class UserData
{
    public KingdomStat kingdomStat;

    public string userid;
    public string username;
    public string usertoken;

    public float gold;
    public float diamond;
    public float playtime;
    public bool isVipMember;
    
    public UserData()
    {
        kingdomStat = new KingdomStat();
    }
}
