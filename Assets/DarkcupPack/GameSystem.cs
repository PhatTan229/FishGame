using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    public static UserData userdata;

    public static List<GameObject> poolObjects;
    public static List<string> poolNames;

    public static readonly string USER_DATA_FILE_NAME = "userdata";

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }

        poolObjects = new List<GameObject>();
        poolNames = new List<string>();
    }

    private void OnLevelWasLoaded(int level)
    {
        poolObjects = new List<GameObject>();
        poolNames = new List<string>();
    }

    public static GameObject LoadPool(string poolName, Vector3 position) {
        for (int i = 0; i < poolNames.Count; i++) {
            if (string.Compare(poolNames[i], poolName) == 0 && poolObjects[i].activeSelf == false) {
                poolObjects[i].SetActive(true);
                poolObjects[i].transform.position = position;
                return poolObjects[i];
            }
        }
        GameObject src = Resources.Load<GameObject>(poolName) as GameObject;

        GameObject obj = Instantiate(src, position, src.transform.rotation);
        poolNames.Add(poolName);
        poolObjects.Add(obj);
        return obj;
    }

    public static Vector3 GoToTargetVector(Vector3 current, Vector3 target, float speed, bool isFlying = false) {
        float distanceToTarget = Vector3.Distance(current, target);
        if (distanceToTarget < 0.1f)
            return new Vector3(0, 0);

        Vector3 vectorToTarget = target - current;

        vectorToTarget = vectorToTarget * speed / distanceToTarget;

        return vectorToTarget;
    }

    public static void DelayCall(float time, Action doneAction)
    {
        Instance.StartCoroutine(DelayCallCoroutine(time, doneAction));
    }

    static IEnumerator DelayCallCoroutine(float time, Action doneAction)
    {
        yield return new WaitForSeconds(time);
        doneAction.Invoke();
    }

    public IEnumerator IncreaseNumberEffect(TextMeshProUGUI txtNumber, int startGold, int endGold, float effectTime) {
        int increase = (int)((endGold - startGold) / (effectTime / Time.deltaTime));
        if (increase == 0) {
            increase = endGold > startGold ? 1 : -1;
        }
        int gold = startGold;
        bool loop = true;
        while (loop) {
            gold += increase;

            if (startGold < endGold) {
                loop = gold < endGold;

                if (gold > endGold) gold = endGold;
            } else {
                loop = gold > endGold;

                if (gold < endGold) gold = endGold;
            }

            txtNumber.text = gold.ToString();

            yield return new WaitForEndOfFrame();
        }
    }

    public static void SaveUserDataToLocal() {
        string json = JsonConvert.SerializeObject(GameSystem.userdata);
        string path = FileUtilities.GetWritablePath(GameSystem.USER_DATA_FILE_NAME);

        FileUtilities.SaveFile(System.Text.Encoding.UTF8.GetBytes(json), path, true);
    }

    public static void LoadUserData()
    {
        if (!FileUtilities.IsFileExist(GameSystem.USER_DATA_FILE_NAME))
        {
            GameSystem.userdata = new UserData();
            GameSystem.SaveUserDataToLocal();
        }
        else
        {
            GameSystem.userdata = FileUtilities.DeserializeObjectFromFile<UserData>(GameSystem.USER_DATA_FILE_NAME);
        }
    }

    public static int GetSortingOrder(Transform t) {
        return -(int)(t.position.y * 100);
    }

    public static string ShortenNumer(float moneyIn, int digitNumber = 2) {
        const float THOUNDSAND = 1000;
        const float MILION = 1000000;
        const float BILLION = 1000000000;
        const float TRILLION = 1000000000000;
        const float TRILLION_1 = TRILLION * 1000;
        const float TRILLION_2 = TRILLION_1 * 1000;

        string FORMAT = "F" + digitNumber;

        if (moneyIn < THOUNDSAND) {
            return ((int)moneyIn).ToString();
        }

        if (moneyIn < MILION) {
            return (moneyIn / THOUNDSAND).ToString(FORMAT) + "K";
        }

        if (moneyIn < BILLION) {
            return (moneyIn / MILION).ToString(FORMAT) + "M";
        }

        if (moneyIn < TRILLION) {
            return (moneyIn / BILLION).ToString(FORMAT) + "B";
        }

        if (moneyIn < TRILLION_1) {
            return (moneyIn / TRILLION).ToString(FORMAT) + "T";
        }

        if (moneyIn < TRILLION_2) {
            return (moneyIn / TRILLION_1).ToString(FORMAT) + "AA";
        }

        return "Infinity!";
    }

    public static float GetDiamondPrice(int level) {

        float price = Mathf.Pow(1.2f, level);

        return price;
    }

    public static int GetDayCode(DateTime date) {
        string year = date.Year.ToString();

        string month = date.Month.ToString();
        if (date.Month < 10)
            month = "0" + month;

        string day = date.Day.ToString();
        if (date.Day < 10)
            day = "0" + day;

        return int.Parse(year + month + day);
    }

    public void ReceiveReward() {
        List<int> rewards = new List<int> {
            UnityEngine.Random.Range(1,4),
            UnityEngine.Random.Range(1,4),
            UnityEngine.Random.Range(1,4),
            UnityEngine.Random.Range(1,3),
            0,0,0,0
        };

        GameSystem.SaveUserDataToLocal();
    }

    public void ClosePopup(GameObject popup) {
        popup.SetActive(false);
    }
}