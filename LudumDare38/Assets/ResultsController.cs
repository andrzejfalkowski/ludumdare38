using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsController : MonoBehaviour {
    
    [SerializeField]
    private TextMeshProUGUI ResultText;
    [SerializeField]
    private TextMeshProUGUI TypeText;
    [SerializeField]
    private TextMeshProUGUI TryAgainText;
    [SerializeField]
    private TextMeshProUGUI TryAgainHarderText;
    [SerializeField]
    private TextMeshProUGUI BackText;

    // The headline.
    private const string VICTORY_TITLE = "Victory";
    private const string DEFEAT_TITLE = "Game Over";
    // Will be part of the subtitle explanation:
    private const string VICTORY_SUBTITLE = "victory";
    private const string DEFEAT_SUBTITLE = "defeat";
    // Explanations for specific types of ending the game.
    private const string SUBTITLE_ECONOMIC = "Economic {0}: population of {1}!";
    private const string SUBTITLE_MILITARY = "Military {0}: eliminate all enemies!";

    // For text mesh pro gradients - victory.
    [SerializeField]
    private Color victoryGradientTopLeftColor;
    [SerializeField]
    private Color victoryGradientTopRightColor;
    [SerializeField]
    private Color victoryGradientBottomLeftColor;
    [SerializeField]
    private Color victoryGradientBottomRightColor;

    // For text mesh pro gradients - defeat.
    [SerializeField]
    private Color defeatGradientTopLeftColor;
    [SerializeField]
    private Color defeatGradientTopRightColor;
    [SerializeField]
    private Color defeatGradientBottomLeftColor;
    [SerializeField]
    private Color defeatGradientBottomRightColor;

    public void Show(EGameOverResult result, EGameOverType type)
    {
        // Texts.
        ResultText.text = GetTitleForResult(result);
        TypeText.text = string.Format(GetSubtitleForType(type), GetSubtitleForResult(result));

        // Colors.
        SetColorsForResult(ResultText, result);
        SetColorsForResult(TypeText, result);
        SetColorsForResult(BackText, result);

        SetColorsForResult(TryAgainHarderText, EGameOverResult.Victory);
        SetColorsForResult(TryAgainText, EGameOverResult.Defeat);

        // Result-specific buttons.
        bool victory = (result == EGameOverResult.Victory);
        TryAgainText.gameObject.SetActive(!victory);
        TryAgainHarderText.gameObject.SetActive(victory);

        gameObject.SetActive(true);
    }

    private string GetTitleForResult(EGameOverResult result)
    {
        switch (result)
        {
            case EGameOverResult.Victory:
                return VICTORY_TITLE;
            case EGameOverResult.Defeat:
                return DEFEAT_TITLE;
            default:
                return "";
        }
    }

    private string GetSubtitleForResult(EGameOverResult result)
    {
        switch (result)
        {
            case EGameOverResult.Victory:
                return VICTORY_SUBTITLE;
            case EGameOverResult.Defeat:
                return DEFEAT_SUBTITLE;
            default:
                return "";
        }
    }

    private string GetSubtitleForType(EGameOverType type)
    {
        switch (type)
        {
            case EGameOverType.Economic:
                return string.Format(SUBTITLE_ECONOMIC, "{0}", Constants.ECONOMIC_VICTORY_THRESHOLD);
            case EGameOverType.Military:
                return SUBTITLE_MILITARY;
            default:
                return "";
        }
    }

    private void SetColorsForResult(TextMeshProUGUI text, EGameOverResult result)
    {
        VertexGradient gradient = new VertexGradient();
        switch (result)
        {
            case EGameOverResult.Victory:
                gradient.topLeft = victoryGradientTopLeftColor;
                gradient.topRight = victoryGradientTopRightColor;
                gradient.bottomLeft = victoryGradientBottomLeftColor;
                gradient.bottomRight = victoryGradientBottomRightColor;
                break;
            case EGameOverResult.Defeat:
                gradient.topLeft = defeatGradientTopLeftColor;
                gradient.topRight = defeatGradientTopRightColor;
                gradient.bottomLeft = defeatGradientBottomLeftColor;
                gradient.bottomRight = defeatGradientBottomRightColor;
                break;
        }
        text.colorGradient = gradient;
    }
}
