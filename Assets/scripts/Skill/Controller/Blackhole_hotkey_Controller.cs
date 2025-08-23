using TMPro;
using UnityEngine;

public class Blackhole_hotkey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode hotkey;
    private TextMeshProUGUI text;

    private Transform enemy;
    private Blackhole_Controller blackhole;
    public void Set_Hotkey(KeyCode _hotkey, Transform _enemy, Blackhole_Controller _blackhole)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        sr = GetComponent<SpriteRenderer>();
        hotkey = _hotkey;
        text.text = _hotkey.ToString();

        enemy = _enemy;
        blackhole = _blackhole;
    }

    private void Update()
    {
        if (Input.GetKeyDown(hotkey))
        {
            blackhole.AddEnemyList(enemy);
            text.color = Color.clear;
            sr.color = Color.clear;
        }
    }

}
