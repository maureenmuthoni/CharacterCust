using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceGUI : MonoBehaviour
{
    public int KevinCurHealth, KevinMaxHealth;
    public Vector2 targetPos;
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        if (KevinCurHealth < 0)
        {
            KevinCurHealth = 0;
        }
        if (KevinCurHealth > KevinMaxHealth)
        {
            KevinCurHealth = KevinMaxHealth;
        }
    }
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        float size = CalculateSize();
        if (size > 0)
            GUI.Box(new Rect(targetPos.x - 0.5f * scrW, -targetPos.y + scrH * 8f, (KevinCurHealth * scrW / KevinMaxHealth)*size, scrH * 0.25f), "");
    }

    float CalculateSize()
    {
        float size = 2f;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= 15)
            size -= distance * 0.1f;
        else if (distance > 15)
            size = 0;
        return size;
    }
}
