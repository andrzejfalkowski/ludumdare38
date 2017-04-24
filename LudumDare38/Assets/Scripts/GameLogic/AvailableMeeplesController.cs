using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableMeeplesController : MonoBehaviour {

    static public AvailableMeeplesController Instance;
    [SerializeField]
    protected GameObject[] Icons;
    [SerializeField]
    protected GameObject ReadyLabel;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetIcons(GameplayManager.Instance.Player.RemainingStartingMeeples);
    }

    public void SetIcons(int number)
    {
        ReadyLabel.SetActive(number > 0);
        foreach (GameObject icon in Icons)
        {
            // Activate right amount of icons and deactivate others.
            icon.SetActive(number-- > 0);
        }
    }
}
