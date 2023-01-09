
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    // Use this for initialization
    int times = 1;//游戏进行的次数，每个人点击一次，这个值加一
    int num = 0;//记录被点击的格子数，如果9个格子都被点击完，双方还是没有分出胜负，就算平局
    int[,] tic = new int[3, 3] {
        {0, 0, 0} ,   /*  初始化索引号为 0 的行 */
        {0, 0, 0} ,   /*  初始化索引号为 1 的行 */
        {0, 0, 0}   /*  初始化索引号为 2 的行 */
    };
    //这个二维数组来保持当前的游戏状态，每个值都有三种可能
    //0：这个格子没被点击过
    //1：这个格子被玩家一点击过
    //2：这个格子被玩家二点击过
    void Start()
    {
    }
    void update()
    {

    }
    int win()
    {
        for (int i = 0; i < 3; i++)
        {
            if (tic[i, 0] == tic[i, 1] && tic[i, 1] == tic[i, 2])
            {
                if (tic[i, 0] != 0)
                {
                    return tic[i, 0];
                }
            }
            //三个相同的非零数连成了一条横线，为1则玩家1胜利，为2则玩家2胜利
            if (tic[0, i] == tic[1, i] && tic[1, i] == tic[2, i])
            {
                if (tic[0, i] != 0)
                {
                    return tic[0, i];
                }
            }
            //三个相同的非零数连成了一条竖线，为1则玩家1胜利，为2则玩家2胜利
        }
        if ((tic[0, 0] == tic[1, 1] && tic[1, 1] == tic[2, 2]) || (tic[2, 0] == tic[1, 1] && tic[1, 1] == tic[0, 2]))
        {
            if (tic[1, 1] != 0)
            {
                return tic[1, 1];
            }
        }
        //三个相同的非零数连成了一条斜线，为1则玩家1胜利，为2则玩家2胜利
        return 0;
    }
    void restart()//重新开始，不但要将状态清0，还要把已经被点击过的格子也清0，否则在判断平局的时候会出现问题
    {
        times = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tic[i, j] = 0;
            }
        }
        num = 0;
    }
    void OnGUI()
    {
        GUI.skin.button.fontSize = 30;
        GUI.skin.box.fontSize = 30;
        GUI.Box(new Rect(Screen.width / 2 - 100, 100, 200, 100), "Tic-Tac-Toe");
        if (GUI.Button(new Rect(Screen.width / 2 - 100, 800, 200, 100), "restart"))
        {
            restart();
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tic[i, j] == 1)
                {

                    GUI.Button(new Rect(Screen.width / 2 - 300 + 200 * i, 200 + j * 200, 200, 200), "O");
                }
                if (tic[i, j] == 2)
                {

                    GUI.Button(new Rect(Screen.width / 2 - 300 + 200 * i, 200 + j * 200, 200, 200), "X");
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 300 + 200 * i, 200 + j * 200, 200, 200), ""))
                {

                    if (times % 2 == 0)
                    {
                        tic[i, j] = 2;
                    }
                    else
                    {
                        tic[i, j] = 1;
                    }
                    //在这里使用times%2来记录次序
                    times++;
                    num++;
                }

            }
        }
        if (win() == 0 && num == 9)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, 400, 300, 100), "Draw");
        }//九个格子都被点击了，却还是每分出胜负，则平局
        if (win() == 1)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, 400, 300, 100), "Player 1 win!");
        }//玩家1胜利
        if (win() == 2)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, 400, 300, 100), "Player 2 win!");
        }//玩家2胜利
    }

}