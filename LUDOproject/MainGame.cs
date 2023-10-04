using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Timers;

namespace LUDOproject
{
    public partial class MainGame : Form
    {
        short Mode, Player, Sound;
        int Dice, Team=0, Step=0, Round=0;
        int [,] TeamPieceStatus = new int [4,4] { {-1, -1, -1, -1},            
                                                {-1, -1, -1, -1},
                                                {-1, -1, -1, -1},
                                                {-1, -1, -1, -1} };          // 紀錄棋子還在家嗎


        Game_setting setting = new Game_setting();

        public MainGame()
        {
            InitializeComponent();
        }

        public bool LoadSettingToTips()                 // 將Setting遊戲設置顯示至置底提示欄
        {
            int SettingCheck = 0;
            toolStripStatusLabel1.ForeColor = Color.Black;
            toolStripStatusLabel1.Text = "【載入遊戲設置】";
            if (Mode == 1)
            {
                toolStripStatusLabel1.Text += "單機多人模式／";
                SettingCheck ++;
            }
            else
            {
                toolStripStatusLabel1.Text += "邊緣人模式／";
                SettingCheck ++;
            }
            switch (Player)
            {
                case 2:
                    toolStripStatusLabel1.Text += "Player 2／";
                    SettingCheck ++;
                    break;
                case 3:
                    toolStripStatusLabel1.Text += "Player 3／";
                    SettingCheck ++;
                    break;
                case 4:
                    toolStripStatusLabel1.Text += "Player 4／";
                    SettingCheck ++;
                    break;
                default:
                    toolStripStatusLabel1.Text += "玩家數設置載入錯誤／";
                    break;
            }
            if(Sound == 1)
            {
                toolStripStatusLabel1.Text += "音效開啟";
                SettingCheck ++;
            }
            else
            {
                toolStripStatusLabel1.Text += "音效關閉";
                SettingCheck ++;
            }
            
            if (SettingCheck == 3)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
        public void GameOverCheck(int Team)
        {
            int InFinalPoint=0;
            for(int i = 0; i < 4; i++)
            {
                if(TeamPieceStatus[Team,i] == 56)
                {
                    InFinalPoint++;
                }
            }
            if (InFinalPoint >= 4)
            {
                switch (Team)
                {
                    case 0:
                        MessageBox.Show("黃隊勝利!!~");
                        break;
                    case 1:
                        MessageBox.Show("綠隊勝利!!~");
                        break;
                    case 2:
                        MessageBox.Show("紅隊勝利!!~");
                        break;
                    case 3:
                        MessageBox.Show("藍隊勝利!!~");
                        break;
                }
                toolStripStatusLabel1.Text = "【遊戲結束】Game Over!! 感謝您的遊玩!! ";
            }
        }

        public void PlayerMove(int Team, int piece, int move)     // 處理棋子移動。
        {
            int timedelay = 300;
            if (move == -99)         // 輸入move == -99時，即直接要求回家。
            {
                switch (Team)
                {
                    case 0:
                        switch (piece)
                        {
                            case 0:
                                TeamPieceStatus[Team, piece] = -1;
                                Y0.Location = new Point(60, 344);
                                break;
                            case 1:
                                TeamPieceStatus[Team, piece] = -1;
                                Y1.Location = new Point(125, 344);
                                break;
                            case 2:
                                TeamPieceStatus[Team, piece] = -1;
                                Y2.Location = new Point(60, 405);
                                break;
                            case 3:
                                TeamPieceStatus[Team, piece] = -1;
                                Y3.Location = new Point(125, 405);
                                break;
                        }
                        break;
                    case 1:
                        switch (piece)
                        {
                            case 0:
                                TeamPieceStatus[Team, piece] = -1;
                                G0.Location = new Point(60, 45);
                                break;
                            case 1:
                                TeamPieceStatus[Team, piece] = -1;
                                G1.Location = new Point(125, 45);
                                break;
                            case 2:
                                TeamPieceStatus[Team, piece] = -1;
                                G2.Location = new Point(60, 108);
                                break;
                            case 3:
                                TeamPieceStatus[Team, piece] = -1;
                                G3.Location = new Point(125, 108);
                                break;
                        }
                        break;
                    case 2:
                        switch (piece)
                        {
                            case 0:
                                TeamPieceStatus[Team, piece] = -1;
                                R0.Location = new Point(355, 45);
                                break;
                            case 1:
                                TeamPieceStatus[Team, piece] = -1;
                                R1.Location = new Point(419, 45);
                                break;
                            case 2:
                                TeamPieceStatus[Team, piece] = -1;
                                R2.Location = new Point(355, 108);
                                break;
                            case 3:
                                TeamPieceStatus[Team, piece] = -1;
                                R3.Location = new Point(419, 108);
                                break;
                        }
                        break;
                    case 3:
                        switch (piece)
                        {
                            case 0:
                                TeamPieceStatus[Team, piece] = -1;
                                B0.Location = new Point(355, 344);
                                break;
                            case 1:
                                TeamPieceStatus[Team, piece] = -1;
                                B1.Location = new Point(419, 344);
                                break;
                            case 2:
                                TeamPieceStatus[Team, piece] = -1;
                                B2.Location = new Point(355, 405);
                                break;
                            case 3:
                                TeamPieceStatus[Team, piece] = -1;
                                B3.Location = new Point(419, 405);
                                break;
                        }
                        break;
                }
            }
            
            
            int[] Yx = new int[57] { 206, 206, 206, 206, 206, 175, 143, 110, 78, 44, 10, 10, 10, 44, 78, 110, 143, 175, 206, 206, 206, 206, 206, 206, 239, 273, 273, 273, 273, 273, 273, 306, 338, 371, 404, 437, 469, 469, 469, 437, 404, 371, 338, 306, 273, 273, 273, 273, 273, 273, 239, 239, 239, 239, 239, 239, 239 };
            int[] Yy = new int[57] { 428, 395, 362, 330, 296, 266, 266, 266, 266, 266, 266, 232, 198, 198, 198, 198, 198, 198, 163, 133, 100, 67, 34, 1, 1, 1, 34, 67, 100, 133, 163, 198, 198, 198, 198, 198, 198, 232, 266, 266, 266, 266, 266, 266, 296, 330, 362, 395, 428, 459, 459, 428, 395, 362, 330, 296, 266 };

            int[] Gx = new int[57] { 44, 78, 110, 143, 175, 206, 206, 206, 206, 206, 206, 239, 273, 273, 273, 273, 273, 273, 306, 338, 371, 404, 437, 469, 469, 469, 437, 404, 371, 338, 306, 273, 273, 273, 273, 273, 273, 239, 206, 206, 206, 206, 206, 206, 175, 143, 110, 78, 44, 10, 10, 44, 78, 110, 143, 175, 206 };
            int[] Gy = new int[57] { 198, 198, 198, 198, 198, 163, 133, 100, 67, 34, 1, 1, 1, 34, 67, 100, 133, 163, 198, 198, 198, 198, 198, 198, 232, 266, 266, 266, 266, 266, 266, 296, 330, 362, 395, 428, 459, 459, 459, 428, 395, 362, 330, 296, 266, 266, 266, 266, 266, 266, 232, 232, 232, 232, 232, 232, 232 };

            int[] Rx = new int[57] { 273, 273, 273, 273, 273, 306, 338, 371, 404, 437, 469, 469, 469, 437, 404, 371, 338, 306, 273, 273, 273, 273, 273, 273, 239, 206, 206, 206, 206, 206, 206, 175, 143, 110, 78, 44, 10, 10, 10, 44, 78, 110, 143, 175, 206, 206, 206, 206, 206, 206, 239, 239, 239, 239, 239, 239, 239 };
            int[] Ry = new int[57] { 34, 67, 100, 133, 163, 198, 198, 198, 198, 198, 198, 232, 266, 266, 266, 266, 266, 266, 296, 330, 362, 395, 428, 459, 459, 459, 428, 395, 362, 330, 296, 266, 266, 266, 266, 266, 266, 232, 198, 198, 198, 198, 198, 198, 163, 133, 100, 67, 34, 1, 1, 34, 67, 100, 133, 163, 198 };

            int[] Bx = new int[57] { 437, 404, 371, 338, 306, 273, 273, 273, 273, 273, 273, 239, 206, 206, 206, 206, 206, 206, 175, 143, 110, 78, 44, 10, 10, 10, 44, 78, 110, 143, 175, 206, 206, 206, 206, 206, 206, 239, 273, 273, 273, 273, 273, 273, 306, 338, 371, 404, 437, 469, 469, 437, 404, 371, 338, 306, 273 };
            int[] By = new int[57] { 266, 266, 266, 266, 266, 296, 330, 362, 395, 428, 459, 459, 459, 428, 395, 362, 330, 296, 266, 266, 266, 266, 266, 266, 232, 198, 198, 198, 198, 198, 198, 163, 133, 100, 67, 34, 1, 1, 1, 34, 67, 100, 133, 163, 198, 198, 198, 198, 198, 198, 232, 232, 232, 232, 232, 232, 232 };

            if (move == -1)         // 輸入move == -1時，動畫處理走回家。
            {
                switch (Team)
                {
                    case 0:
                        switch (piece)
                        {
                            case 0:
                                for (; TeamPieceStatus[Team, piece] > -1; )
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        Y0.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 1:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        Y1.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 2:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        Y2.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 3:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        Y3.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                        }
                        break;
                    case 1:
                        switch (piece)
                        {
                            case 0:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        G0.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 1:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        G1.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 2:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        G2.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 3:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        G3.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                        }
                        break;
                    case 2:
                        switch (piece)
                        {
                            case 0:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        R0.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 1:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        R1.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 2:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        R2.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 3:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        R3.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                        }
                        break;
                    case 3:
                        switch (piece)
                        {
                            case 0:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        B0.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 1:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        B1.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 2:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        B2.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                            case 3:
                                for (; TeamPieceStatus[Team, piece] > -1;)
                                {
                                    TeamPieceStatus[Team, piece]--;
                                    if (TeamPieceStatus[Team, piece] == -1)
                                    {
                                        PlayerMove(Team, piece, -99);
                                    }
                                    else
                                    {
                                        B3.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                    }
                                    Task.Delay(timedelay).Wait();
                                }
                                break;
                        }
                        break;
                }
            }

            move++;

            switch (Team)
            {
                case 0:
                    switch (piece)
                    {
                        case 0:
                            for (int i = 0; i < move; i++) {
                                TeamPieceStatus[Team, piece]++;
                                Y0.Location = new Point( Yx[ TeamPieceStatus[Team,piece] ] , Yy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                                    }
                            GameOverCheck(Team);

                            break;
                        case 1:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                Y1.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 2:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                Y2.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 3:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                Y3.Location = new Point(Yx[TeamPieceStatus[Team, piece]], Yy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                    }
                    break;
                case 1:
                    switch (piece)
                    {
                        case 0:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                G0.Location = new Point(Gx[TeamPieceStatus[Team, piece]], Gy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 1:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                G1.Location = new Point(Gx[TeamPieceStatus[Team, piece]], Gy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();

                            }
                            break;
                        case 2:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                G2.Location = new Point(Gx[TeamPieceStatus[Team, piece]], Gy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 3:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                G3.Location = new Point(Gx[TeamPieceStatus[Team, piece]], Gy[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                    }
                    break;
                case 2:
                    switch (piece)
                    {
                        case 0:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                R0.Location = new Point(Rx[TeamPieceStatus[Team, piece]], Ry[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 1:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                R1.Location = new Point(Rx[TeamPieceStatus[Team, piece]], Ry[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 2:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                R2.Location = new Point(Rx[TeamPieceStatus[Team, piece]], Ry[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 3:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                R3.Location = new Point(Rx[TeamPieceStatus[Team, piece]], Ry[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                    }
                    break;
                case 3:
                    switch (piece)
                    {
                        case 0:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                B0.Location = new Point(Bx[TeamPieceStatus[Team, piece]], By[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 1:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                B1.Location = new Point(Bx[TeamPieceStatus[Team, piece]], By[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 2:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                B2.Location = new Point(Bx[TeamPieceStatus[Team, piece]], By[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                        case 3:
                            for (int i = 0; i < move; i++)
                            {
                                TeamPieceStatus[Team, piece]++;
                                B3.Location = new Point(Bx[TeamPieceStatus[Team, piece]], By[TeamPieceStatus[Team, piece]]);
                                Task.Delay(timedelay).Wait();
                            }
                            GameOverCheck(Team);

                            break;
                    }
                    break;
            }

        }

        public void HitOtherPiece(int Team, int piece)     //採點位置確認
        {
            int[] SpecialStatus = new int[14] { 0, 8, 13, 21, 26, 34, 39, 47, 51, 52, 53, 54, 55, 56 };      // 特殊格
            int OnSpecial = 0;        //特殊格確認

            for (int i = 0; i < 14; i++) {
                if (TeamPieceStatus[Team, piece] == SpecialStatus[i])
                {
                    OnSpecial++;
                } 
            }
            if(OnSpecial == 0)      // 若棋子不在特殊格上，即在空白格上。
            {
                for(int i = 0; i < 4; i++)
                {
                    if (i == Team)      // 若檢查時選擇隊伍為玩家隊伍，則跳過檢查其他隊伍的棋子
                        i++;
                    if(Player == 2)     //玩家數2的話
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (Team < i)
                            {
                                if (TeamPieceStatus[Team, piece] == (TeamPieceStatus[i, j]-26))
                                    PlayerMove(i, j, -99);
                            }
                            else
                            {
                                if ((TeamPieceStatus[Team, piece]+26) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                        }
                    }
                    if (Player == 3)        //玩家數3人的話
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if(Team == 0)
                            {
                                if (i == 1)
                                {
                                    if ((TeamPieceStatus[Team, piece] - 13) == TeamPieceStatus[i, j])
                                        PlayerMove(i, j, -99);
                                }
                                if (i == 2)
                                {
                                    if ((TeamPieceStatus[Team, piece] - 26) == TeamPieceStatus[i, j])
                                        PlayerMove(i, j, -99);
                                }
                            }else if(Team == 1)
                            {
                                if (i == 0)
                                {
                                    if ((TeamPieceStatus[Team, piece] + 13) == TeamPieceStatus[i, j])
                                        PlayerMove(i, j, -99);
                                }
                                if (i == 2)
                                {
                                    if ((TeamPieceStatus[Team, piece] - 13) == TeamPieceStatus[i, j])
                                        PlayerMove(i, j, -99);
                                }
                            }
                            else if (Team == 2)
                            {
                                if (i == 1)
                                {
                                    if ((TeamPieceStatus[Team, piece] + 13) == TeamPieceStatus[i, j])
                                        PlayerMove(i, j, -99);
                                }
                                if (i == 0)
                                {
                                    if ((TeamPieceStatus[Team, piece] + 26) == TeamPieceStatus[i, j])
                                        PlayerMove(i, j, -99);
                                }
                            }
                        }
                    }
                    if (Player == 4)        //玩家數4人的話
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (Team == 0 && i == 1)
                            {
                                if ((TeamPieceStatus[Team, piece] - 13) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 0 && i == 2)
                            {
                                if ((TeamPieceStatus[Team, piece] - 26) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 0 && i == 3)
                            {
                                if ((TeamPieceStatus[Team, piece] - 39) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 1 && i == 0)
                            {
                                if ((TeamPieceStatus[Team, piece] + 13) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 1 && i == 2)
                            {
                                if ((TeamPieceStatus[Team, piece] - 13) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 1 && i == 3)
                            {
                                if ((TeamPieceStatus[Team, piece] - 26) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 2 && i == 0)
                            {
                                if ((TeamPieceStatus[Team, piece] + 26) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 2 && i == 1)
                            {
                                if ((TeamPieceStatus[Team, piece] + 13) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 2 && i == 3)
                            {
                                if ((TeamPieceStatus[Team, piece] - 13) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 3 && i == 0)
                            {
                                if ((TeamPieceStatus[Team, piece] + 39) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 3 && i == 1)
                            {
                                if ((TeamPieceStatus[Team, piece] + 26) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                            if (Team == 3 && i == 2)
                            {
                                if ((TeamPieceStatus[Team, piece] + 13) == TeamPieceStatus[i, j])
                                    PlayerMove(i, j, -99);
                            }
                        }
                    }
                }
            }
        }

        public short PlayerChange(short Players)        // 處理Player數，決定棋子顯示數目。
        {
            if(Player == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        PlayerMove(i, j, -99);
                    }
                }
                Y0.Visible = true;
                Y1.Visible = true;
                Y2.Visible = true;
                Y3.Visible = true;
                R0.Visible = true;
                R1.Visible = true;
                R2.Visible = true;
                R3.Visible = true;
                G0.Visible = false;
                G1.Visible = false;
                G2.Visible = false;
                G3.Visible = false;
                B0.Visible = false;
                B1.Visible = false;
                B2.Visible = false;
                B3.Visible = false;
                return 1;
            }
            else if (Player == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        PlayerMove(i, j, -99);
                    }
                }
                Y0.Visible = true;
                Y1.Visible = true;
                Y2.Visible = true;
                Y3.Visible = true;
                R0.Visible = true;
                R1.Visible = true;
                R2.Visible = true;
                R3.Visible = true;
                G0.Visible = true;
                G1.Visible = true;
                G2.Visible = true;
                G3.Visible = true;
                B0.Visible = false;
                B1.Visible = false;
                B2.Visible = false;
                B3.Visible = false;
                return 1;
            }else if(Player == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        PlayerMove(i, j, -99);
                    }
                }
                Y0.Visible = true;
                Y1.Visible = true;
                Y2.Visible = true;
                Y3.Visible = true;
                R0.Visible = true;
                R1.Visible = true;
                R2.Visible = true;
                R3.Visible = true;
                G0.Visible = true;
                G1.Visible = true;
                G2.Visible = true;
                G3.Visible = true;
                B0.Visible = true;
                B1.Visible = true;
                B2.Visible = true;
                B3.Visible = true;
            }
            else
            {
                return 0;
            }
            return 0;
        }

        public void TeamPieceEnable (int Team)          // 指定隊伍全數棋子Enable，代號4為全數False
        {
            switch (Team)
            {
                case 0:
                    Y0.Enabled = true; Y1.Enabled = true; Y2.Enabled = true; Y3.Enabled = true;
                    G0.Enabled = false; G1.Enabled = false; G2.Enabled = false; G3.Enabled = false;
                    R0.Enabled = false; R1.Enabled = false; R2.Enabled = false; R3.Enabled = false;
                    B0.Enabled = false; B1.Enabled = false; B2.Enabled = false; B3.Enabled = false;
                    break;
                case 1:
                    Y0.Enabled = false; Y1.Enabled = false; Y2.Enabled = false; Y3.Enabled = false;
                    G0.Enabled = true; G1.Enabled = true; G2.Enabled = true; G3.Enabled = true;
                    R0.Enabled = false; R1.Enabled = false; R2.Enabled = false; R3.Enabled = false;
                    B0.Enabled = false; B1.Enabled = false; B2.Enabled = false; B3.Enabled = false;
                    break;
                case 2:
                    Y0.Enabled = false; Y1.Enabled = false; Y2.Enabled = false; Y3.Enabled = false;
                    G0.Enabled = false; G1.Enabled = false; G2.Enabled = false; G3.Enabled = false;
                    R0.Enabled = true; R1.Enabled = true; R2.Enabled = true; R3.Enabled = true;
                    B0.Enabled = false; B1.Enabled = false; B2.Enabled = false; B3.Enabled = false;
                    break;
                case 3:
                    Y0.Enabled = false; Y1.Enabled = false; Y2.Enabled = false; Y3.Enabled = false;
                    G0.Enabled = false; G1.Enabled = false; G2.Enabled = false; G3.Enabled = false;
                    R0.Enabled = false; R1.Enabled = false; R2.Enabled = false; R3.Enabled = false;
                    B0.Enabled = true; B1.Enabled = true; B2.Enabled = true; B3.Enabled = true;
                    break;
                case 4:
                    Y0.Enabled = false; Y1.Enabled = false; Y2.Enabled = false; Y3.Enabled = false;
                    G0.Enabled = false; G1.Enabled = false; G2.Enabled = false; G3.Enabled = false;
                    R0.Enabled = false; R1.Enabled = false; R2.Enabled = false; R3.Enabled = false;
                    B0.Enabled = false; B1.Enabled = false; B2.Enabled = false; B3.Enabled = false;
                    break;
            }
        }

        public void OnePieceEnable (int Team,int Piece, bool MoveOK)        // 指定單顆棋子Enable
        {
            switch (Team)
            {
                case 0:
                    switch (Piece)
                    {
                        case 0:
                            Y0.Enabled = MoveOK;
                            break;
                        case 1:
                            Y1.Enabled = MoveOK;
                            break;
                        case 2:
                            Y2.Enabled = MoveOK;
                            break;
                        case 3:
                            Y3.Enabled = MoveOK;
                            break;
                    }
                    break;
                case 1:
                    switch (Piece)
                    {
                        case 0:
                            G0.Enabled = MoveOK;
                            break;
                        case 1:
                            G1.Enabled = MoveOK;
                            break;
                        case 2:
                            G2.Enabled = MoveOK;
                            break;
                        case 3:
                            G3.Enabled = MoveOK;
                            break;
                    }
                    break;
                case 2:
                    switch (Piece)
                    {
                        case 0:
                            R0.Enabled = MoveOK;
                            break;
                        case 1:
                            R1.Enabled = MoveOK;
                            break;
                        case 2:
                            R2.Enabled = MoveOK;
                            break;
                        case 3:
                            R3.Enabled = MoveOK;
                            break;
                    }
                    break;
                case 3:
                    switch (Piece)
                    {
                        case 0:
                            B0.Enabled = MoveOK;
                            break;
                        case 1:
                            B1.Enabled = MoveOK;
                            break;
                        case 2:
                            B2.Enabled = MoveOK;
                            break;
                        case 3:
                            B3.Enabled = MoveOK;
                            break;
                    }
                    break;

            }
        }

        public void TeamTurn(int Step)                                      // 主遊戲處理區塊，控制遊戲隊伍選擇。
        {

            if (Player == 2 && Team == 1)               // 當玩家數只有兩人時，直接跳轉由紅方進行遊戲。
            {
                Team = 2;
            }
            if (Player == 2 && Team == 3)
            {
                Team = 0; Round += 1; toolStripStatusLabel3.Text = "遊戲回合數：" + Round;
            }
            if (Player == 3 && Team == 3)
            {
                Team = 0; Round += 1; toolStripStatusLabel3.Text = "遊戲回合數：" + Round;
            }
            if(Player == 4 && Team == 4)
            {
                Team = 0; Round += 1; toolStripStatusLabel3.Text = "遊戲回合數：" + Round;
            }

            if(Team == 0)                               // 黃方程式區塊
            {
                toolStripStatusLabel1.ForeColor = Color.Goldenrod;
                switch (Step)
                {
                    case 0:
                        RollDiceTimer.Enabled = true;
                        toolStripStatusLabel1.Text = "【黃方回合】請擲骰... ";
                        Task.Delay(500).Wait();
                        DiceButton.Enabled = true;
                        break;
                    case 1:
                        toolStripStatusLabel1.Text = "【黃方回合】請選擇移動棋子。";
                        break;

                }
            }
            if (Team == 1)                               // 綠方程式區塊
            {
                toolStripStatusLabel1.ForeColor = Color.SeaGreen;
                switch (Step)
                {
                    case 0:
                        RollDiceTimer.Enabled = true;
                        toolStripStatusLabel1.Text = "【綠方回合】請擲骰... ";
                        Task.Delay(500).Wait();
                        DiceButton.Enabled = true;
                        break;
                    case 1:
                        toolStripStatusLabel1.Text = "【綠方回合】請選擇移動棋子。";
                        break;

                }
            }
            if (Team == 2)                              // 紅方程式區塊
            {
                toolStripStatusLabel1.ForeColor = Color.Firebrick;
                switch (Step)
                {
                    case 0:
                        RollDiceTimer.Enabled = true;
                        toolStripStatusLabel1.Text = "【紅方回合】請擲骰... ";
                        Task.Delay(500).Wait();
                        DiceButton.Enabled = true;
                        break;
                    case 1:
                        toolStripStatusLabel1.Text = "【紅方回合】請選擇移動棋子。";
                        break;

                }
            }
            if (Team == 3)                              // 藍方程式區塊
            {
                toolStripStatusLabel1.ForeColor = Color.RoyalBlue;
                switch (Step)
                {
                    case 0:
                        RollDiceTimer.Enabled = true;
                        toolStripStatusLabel1.Text = "【藍方回合】請擲骰... ";
                        Task.Delay(500).Wait();
                        DiceButton.Enabled = true;
                        break;
                    case 1:
                        toolStripStatusLabel1.Text = "【藍方回合】請選擇移動棋子。";
                        break;

                }
            }
        }                                                            

        private void RollDiceTimer_Tick(object sender, EventArgs e)             // 骰子亂數計時器
        {
            Random rnd = new Random();
            Dice = rnd.Next(6);
            DiceButton.Image = DiceList.Images[Dice];
            TeamPieceEnable(4);
        }

        private void DiceButton_Click(object sender, EventArgs e)               // 骰子被按下時...
        {
            RollDiceTimer.Enabled = false;
            toolStripStatusLabel1.Text += (Dice+1);                 // 擲骰結果輸出至提示精靈
            DiceButton.Enabled = false;
            Task.Delay(1000).Wait();
            if (Dice == 5)                                  //當骰到6
            {
                TeamTurn(Step = 1);         
                TeamPieceEnable(Team);                      //開放全骰子可選
                for(int i = 0; i < 4; i++)
                {
                    if (TeamPieceStatus[Team, i] + Dice > 55)
                        OnePieceEnable(Team, i, false);
                }
            }
            else                                            //骰到1~5
            {
                int checkTeamAllAtHome = 0;
                for (int i = 0; i < 4; i++)                 
                {
                    if (TeamPieceStatus[Team, i] >= 0 && (TeamPieceStatus[Team, i] + Dice <= 55) )         // 確認各棋子沒有在家 & 骰子數未超出終點
                    {                                                                                      // 就可以開放選取
                        OnePieceEnable(Team, i, true);
                        
                    }
                    if (TeamPieceStatus[Team, i] + Dice > 55)               // 骰子數會超出終點視同待在家，不予選取
                        checkTeamAllAtHome++;
                    if (TeamPieceStatus[Team, i] == -1)     // 當棋子狀態還在家未出發
                    {     
                        OnePieceEnable(Team, i, false);
                        checkTeamAllAtHome++;
                    }
                }
                if (checkTeamAllAtHome >= 4)             // 全都在家時直接跳下一方遊戲
                {
                    Team++; TeamTurn(Step = 0);
                }
                else
                {
                    TeamTurn(Step = 1);
                }
            }
        }

        private void MainGame_Load(object sender, EventArgs e)
        {
            setting.Hide();                             // 預先載入Setting視窗隱藏。
            toolStripStatusLabel3.Text = "遊戲回合數：" + Round;
            TeamPieceEnable(4);
        }

        private void 規則說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Game_help help = new Game_help();
            help.Show();
        }

        private void MainGame_Exit(object sender, EventArgs e)
        {
            setting.Dispose();
        }       // 遊戲主視窗關閉後進行動作。

        private void ToolStripStatusLabel1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("遊戲狀態提示精靈在此...\n\n您可以藉由此處獲得遊戲下一步該如何進行。\n此功能尚在測試階段。");
        }

        private void 開始遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool GameCheck;
            Team = 0; Round = 0; toolStripStatusLabel3.Text = "遊戲回合數：" + Round; Step = 0;
            setting.ShowDialog();
            Mode = setting.GetMode(Mode);
            Player = setting.GetPlayer(Player);
            Sound = setting.GetSound(Sound);
            GameCheck = LoadSettingToTips();
            if(GameCheck == false) {
                do {
                    MessageBox.Show("請確認遊戲設置正確。");
                    setting.ShowDialog();
                    Mode = setting.GetMode(Mode);
                    Player = setting.GetPlayer(Player);
                    Sound = setting.GetSound(Sound);
                    GameCheck = LoadSettingToTips();
                } while (GameCheck != true);
            }
            else {
                開始遊戲ToolStripMenuItem.Text = "遊戲選項";
            }
            PlayerChange(Player);

            Task.Delay(1000).Wait();

            

            DiceButton.Enabled = true;
            TeamTurn(Step);
            
        }

        private void Y0_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【黃方回合】確認移動棋子。";
            
            Task.Delay(1000).Wait();
            if(Dice == 5)
            {
                if(TeamPieceStatus[Team, 0] == -1)
                {
                    PlayerMove(Team, 0, 0);
                }
                else if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 0] >=0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void Y1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【黃方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 1] == -1)
                {
                    PlayerMove(Team, 1, 0);
                }
                else if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void Y2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【黃方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 2] == -1)
                {
                    PlayerMove(Team, 2, 0);
                }
                else if (TeamPieceStatus[Team, 2] >= 0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 2] >= 0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void Y3_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【黃方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 3] == -1)
                {
                    PlayerMove(Team, 3, 0);
                }
                else if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                Team++; TeamTurn(Step = 0);
            }
        }

        private void G0_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【綠方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 0] == -1)
                {
                    PlayerMove(Team, 0, 0);
                }
                else if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void G1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【綠方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 1] == -1)
                {
                    PlayerMove(Team, 1, 0);
                }
                else if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void G2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【綠方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 2] == -1)
                {
                    PlayerMove(Team, 2, 0);
                }
                else if (TeamPieceStatus[Team, 2] >= 0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 2] >= 0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void G3_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【綠方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 3] == -1)
                {
                    PlayerMove(Team, 3, 0);
                }
                else if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                Team++; TeamTurn(Step = 0);
            }
        }

        private void R0_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【紅方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 0] == -1)
                {
                    PlayerMove(Team, 0, 0);
                }
                else if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void R1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【紅方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 1] == -1)
                {
                    PlayerMove(Team, 1, 0);
                }
                else if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void R2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【紅方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if(Dice == 5)
            {
                if(TeamPieceStatus[Team, 2] == -1)
                {
                    PlayerMove(Team, 2, 0);
                }
                else if (TeamPieceStatus[Team, 2] >= 0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 2] >=0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void R3_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【紅方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 3] == -1)
                {
                    PlayerMove(Team, 3, 0);
                }
                else if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                Team++; TeamTurn(Step = 0);
            }
        }

        private void B0_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【藍方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 0] == -1)
                {
                    PlayerMove(Team, 0, 0);
                }
                else if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 0] >= 0)
                {
                    PlayerMove(Team, 0, Dice);
                    HitOtherPiece(Team, 0);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void B1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【藍方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 1] == -1)
                {
                    PlayerMove(Team, 1, 0);
                }
                else if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 1] >= 0)
                {
                    PlayerMove(Team, 1, Dice);
                    HitOtherPiece(Team, 1);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void B2_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【藍方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if(Dice == 5)
            {
                if(TeamPieceStatus[Team, 2] == -1)
                {
                    PlayerMove(Team, 2, 0);
                }
                else if (TeamPieceStatus[Team, 2] >= 0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 2] >=0)
                {
                    PlayerMove(Team, 2, Dice);
                    HitOtherPiece(Team, 2);
                }
                Team++; TeamTurn(Step = 0);
            }
        }
        private void B3_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "【藍方回合】確認移動棋子。";

            Task.Delay(1000).Wait();
            if (Dice == 5)
            {
                if (TeamPieceStatus[Team, 3] == -1)
                {
                    PlayerMove(Team, 3, 0);
                }
                else if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                TeamTurn(Step = 0);
            }
            else
            {
                if (TeamPieceStatus[Team, 3] >= 0)
                {
                    PlayerMove(Team, 3, Dice);
                    HitOtherPiece(Team, 3);
                }
                Team++; TeamTurn(Step = 0);
            }
        }

        private void MainGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)//按下ESC
            {
                Application.Exit();//關閉程式
            }
            else
            {
                if (e.Control == true && e.Alt == true && e.KeyCode == Keys.Z)//按住組合鍵 Ctrl + Alt + T
                {
                    MessageBox.Show("【工程人員偵錯視窗】\n\n" +
                                    "Y0= "+TeamPieceStatus[0,0] + "／Y1= "+TeamPieceStatus[0, 1] + "／Y2= "+TeamPieceStatus[0, 2] + "／Y3= " + TeamPieceStatus[0, 3] +
                                    "\nG0= " + TeamPieceStatus[1, 0] + "／G1= " + TeamPieceStatus[1, 1] + "／G2= " + TeamPieceStatus[1, 2] + "／G3= " + TeamPieceStatus[1, 3]+
                                    "\nR0= " + TeamPieceStatus[2, 0] + "／R1= " + TeamPieceStatus[2, 1] + "／R2= " + TeamPieceStatus[2, 2] + "／R3= " + TeamPieceStatus[2, 3]+
                                    "\nB0= " + TeamPieceStatus[3, 0] + "／B1= " + TeamPieceStatus[3, 1] + "／B2= " + TeamPieceStatus[3, 2] + "／B3= " + TeamPieceStatus[3, 3]+
                                    "\n");
                }
            }
        }
    }
}
