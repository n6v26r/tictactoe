using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textscript : MonoBehaviour
{
    // Start is called before the first frame update

    private static float TIMER_MAX_VAL = 50.0f;

    private static bool showtimer;

    private static bool player;
    private bool pressed;
    private static string[,] matrix = new string[3, 3];

    public int[] pos = new int[2];
    public GameObject line;
    public GameObject winnerText;
    public GameObject displaytimer;


    private static bool gamewon;
    private static int left_tiles;

    private static int[] last = new int[2];

    private static string X = "X";
    private static string O = "O";

    static bool dlts = false;

    private static float timer;
    public GameObject tile;

    void Start()
    {
        Restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (left_tiles == 0 && !gamewon)
            winnerText.GetComponent<TMP_Text>().text = "Draw!";

        if (Input.GetKey(KeyCode.R))
        {
            Restart();
        }
        if (showtimer)
        {
            timer -= Time.deltaTime;
            displaytimer.GetComponent<TMP_Text>().text = ((int)timer / 10).ToString() + "." + ((int)timer % 10).ToString();
        }

        Sugestion();

        if (timer <= 0.0f)
        {
            DeleteSuggestion();
            if (!gamewon)
                GameWon(!player);
        }

        if(dlts)


    }

    private void OnMouseDown()
    {
        if (pressed || gamewon)
            return;
        timer = ResetTimer();
        dlts = true;
        DeleteSuggestion();
        GetComponent<TMP_Text>().text = player ? X : O;
        pressed = true;
        matrix[pos[0], pos[1]] = GetComponent<TMP_Text>().text;
        last = pos;
        left_tiles--;

        int i;
        for (i = 2; i >= 0 && matrix[pos[0], i] == GetComponent<TMP_Text>().text; i--) { }
        if (i < 0)
        {
            GameWon(player);
            line.GetComponent<SpriteRenderer>().enabled = true;
            line.transform.position = new Vector3(line.transform.position.x, transform.position.y, line.transform.position.z);
            line.transform.rotation = Quaternion.Euler(0, 0, 90);
            return;
        }


        for (i = 2; i >= 0 && matrix[i, pos[1]] == GetComponent<TMP_Text>().text; i--) { }

        if (i < 0)
        {
            GameWon(player);
            line.GetComponent<SpriteRenderer>().enabled = true;
            line.transform.position = new Vector3(transform.position.x, line.transform.position.y, line.transform.position.z);
            return;
        }


        for (i = 0; i <= 2 && matrix[i, i] == GetComponent<TMP_Text>().text; i++) { }
        if (i > 2)
        {
            GameWon(player);
            line.GetComponent<SpriteRenderer>().enabled = true;
            line.transform.rotation = Quaternion.Euler(0, 0, 45);
            return;
        }


        for (i = 0; i <= 2 && matrix[2 - i, i] == GetComponent<TMP_Text>().text; i++) { }
        if (i > 2)
        {
            GameWon(player);
            line.GetComponent<SpriteRenderer>().enabled = true;
            line.transform.rotation = Quaternion.Euler(0, 0, -45);
            return;
        }

        player = !player;
    }

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Space) && !gamewon && left_tiles > 0 && last == pos)
        {
            GetComponent<TMP_Text>().text = "";
            pressed = false;
            player = !player;
            matrix[pos[0], pos[1]] = "";
            left_tiles++;
        }
    }

    private void Restart()
    {

        gamewon = false;
        left_tiles = 9;
        GetComponent<TMP_Text>().text = "";
        matrix[pos[0], pos[1]] = "";
        pressed = false;
        showtimer = true;

        player = true;
        line.GetComponent<SpriteRenderer>().enabled = false; line.transform.rotation = Quaternion.Euler(0, 0, 0); line.transform.localPosition = new Vector3(0, 0, 0);
        winnerText.GetComponent<TMP_Text>().text = "";

        timer = ResetTimer();

    }
    private float ResetTimer()
    {
        return TIMER_MAX_VAL;
    }

    private void GameWon(bool player)
    {
        winnerText.GetComponent<TMP_Text>().text = (player ? X : O) + " Won!";
        gamewon = true;
        displaytimer.GetComponent<TMP_Text>().text = "";
        showtimer = false;
    }

    private void Sugestion()
    {
        if(timer<15){
            int i=0;
            int j=0;
            int x = 0;
            for (i = 0; i < 3; i++)
                for (j = 0; j < 3; j++)
                    if (matrix[i, j] == "")
                    {
                        x = i;
                        i = 3;
                        break;
                    }
            if(pos[0]==x && pos[1]==j)
            {  // pozitia curenta
                GetComponent<TMP_Text>().text = "S";
                GetComponent<TMP_Text>().color = Color.green;
            }
        } 
    }
    private void DeleteSuggestion()
    {
           GetComponent<TMP_Text>().text = "";
           GetComponent<TMP_Text>().color = Color.white;
    }
}
