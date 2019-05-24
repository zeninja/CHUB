using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaSlider : MonoBehaviour {
    private static MetaSlider instance;
    public static MetaSlider GetInstance () {
        return instance;
    }

    public int activeSliderIndex = 0;
    float elapsedCompletionPct = 0;
    public float worldCompletionPct;
    public float currentSliderValue;

    int completedWorldCount;

    [System.Serializable]
    public class StageInfo {
        public int world, level;
    }

    [SerializeField]
    public StageInfo stageInfo;

    public GiantSlider[] sliders = new GiantSlider[4];

    public delegate void SliderActivatedEvent ();
    public static event SliderActivatedEvent OnActiveSliderChanged;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy (gameObject);
            }
        }
    }

    void Start () {
        SetSliderActive (activeSliderIndex);
    }

    void OnEnable () {
        GiantSlider.OnValueChanged += UpdateMetaSlider;

    }

void OnDisable()
{
            GiantSlider.OnValueChanged -= UpdateMetaSlider;

}

    public float GetSliderValue (int index) {
        return sliders[index].percent;
    }

    void UpdateMetaSlider () {
        // Find slider value and progress
        currentSliderValue = Mathf.Clamp01 (GetSliderValue (activeSliderIndex));
        worldCompletionPct = elapsedCompletionPct + currentSliderValue / 4;

        // // Set stage info
        // SetStageInfo();

    }

    public int FindSliderIndex (GiantSlider target) {
        for (int i = 0; i < 4; i++) {
            if (sliders[i] == target) {
                return i;
            }
        }
        Debug.LogError ("Slider index not found.");
        return -1;
    }

    public void HandleSliderCompleted (GiantSlider completedSlider) {

        if (activeSliderIndex == FindSliderIndex (completedSlider)) {
            // Debug.Log("Slider completed with index: " + FindSliderIndex(completedSlider));

            // Debug.Log("active slider index matched. slider index increased");
            // currentSliderValue = 0;
            activeSliderIndex++;

            if (activeSliderIndex >= 4) {
                // roll over when you hit 4
                activeSliderIndex = 0;
                completedWorldCount++;

                elapsedCompletionPct = 0;
                worldCompletionPct = 0;

                // Debug.Log("ROLLING OVER TO NEXT WORLD");
            }

            SetStageInfo ();
            SetSliderActive (activeSliderIndex);

            // Debug.Log(stageInfo.world + "-" + stageInfo.level);

            elapsedCompletionPct = activeSliderIndex * .25f; // round the completed value to nearest quarter
        }
    }

    void SetStageInfo () {
        stageInfo.world = completedWorldCount + Mathf.FloorToInt (worldCompletionPct) + 1;
        stageInfo.level = activeSliderIndex + 1;
    }

    // public void ForceNextSlider ( ) {
    //     Debug.Log("Forcing next slider");
    //     activeSliderIndex++;

    //     if (activeSliderIndex >= 4) {
    //         // roll over when you hit 4
    //         activeSliderIndex = 0;
    //         completedWorldCount++;

    //         // Debug.Log("ROLLING OVER TO NEXT WORLD");
    //     }

    //     SetStageInfo ();
    //     SetSliderActive (activeSliderIndex);

    //     elapsedCompletionPct = activeSliderIndex * .25f; // round the completed value to nearest quarter
    // }

    void SetSliderActive (int index) {

        sliders[index].SetActive ();

        if (OnActiveSliderChanged != null) {
            OnActiveSliderChanged ();
        }

        // for (int i = 0; i < 4; i++) {
        // sliders[i]

        // sliders[i].GetComponent<GiantSlider> ().isActive = i == index ? true : false;
        // }

    }

    // public IEnumerator ForceNextSlider() {
    //     while(currentSliderValue < .95) {

    //         yield return new WaitForFixedUpdate();
    //     }
    // }

    public int GetSliderIndex () {
        return activeSliderIndex;
    }

    public float GetCurrentSliderValue () {
        return currentSliderValue;
    }

    public bool StageInfoMatches (StageInfo info) {
        // Debug.Log("Received " + info.world + "-" + info.level);

        return info.world == stageInfo.world && info.level == stageInfo.level;
    }

    public bool InSameWorld (int world) {
        return world == stageInfo.world;
    }


    public Vector3 GetCornerPos() {
        return transform.GetChild(activeSliderIndex).Find("EndCorner").position;
    }

    void OnGUI () {
        GUI.color = Color.black;
        GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), stageInfo.world + "-" + stageInfo.level);
    }

    public Transform playerTarget;
}