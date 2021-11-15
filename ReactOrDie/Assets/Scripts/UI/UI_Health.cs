using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : MonoBehaviour
{
    public RectTransform health;
    public RectTransform border;
    public CharacterStats stats;
    public Transform characterToFollow;
    
    private float m_borderIncrements;
    private float barsSizeDifferenceInPercent;
    private float barsSizeDifference;
    private RectTransform healthBarPos;

    private Vector3 offsetPosition = Vector3.zero;

    private void Start()
    {
        // take the border width and divide by max health to get how large each segment of the border
        // should be for each health point added to max health
        m_borderIncrements = border.sizeDelta.x / stats.maxHealth;
        
        //barsSizeDifferenceInPercent = (border.sizeDelta.x > health.sizeDelta.x) ? 
        //    border.sizeDelta.x / health.sizeDelta.x :
        //    health.sizeDelta.x / border.sizeDelta.x;

        barsSizeDifferenceInPercent = border.sizeDelta.x / health.sizeDelta.x;
        barsSizeDifference = border.sizeDelta.x - health.sizeDelta.x;

        healthBarPos = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float oldSizeX = border.sizeDelta.x;
        // resize the health bar border based on the amount of health we have.
        border.sizeDelta = new Vector2(stats.maxHealth * m_borderIncrements, border.sizeDelta.y);

        // resize the health bar interior based on the players current health level.
        health.sizeDelta = new Vector2((stats.health * m_borderIncrements) - barsSizeDifference /* barsSizeDifferenceInPercent */, health.sizeDelta.y);

        //float centerOffsetX = border.sizeDelta.x / 2;

        // set the position to the character to follow position in screen space. (needs to be screen space for UI to be positioned correctly.)
        healthBarPos.position = Camera.main.WorldToScreenPoint(characterToFollow.position) + new Vector3(/*-centerOffsetX*/0, 30, 0);
    }
}
