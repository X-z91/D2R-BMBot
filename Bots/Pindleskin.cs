﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static app.MapAreaStruc;

namespace app
{
    public class Pindleskin
    {
        Form1 Form1_0;

        public int CurrentStep = 0;
        public bool ScriptDone = false;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void ResetVars()
        {
            CurrentStep = 0;
            ScriptDone = false;
        }

        public void RunScript()
        {
            Form1_0.Town_0.ScriptTownAct = 5; //set to town act 5 when running this script

            if (Form1_0.Town_0.GetInTown())
            {
                Form1_0.SetGameStatus("GO TO WP");
                CurrentStep = 0;

                Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5130, Y = 5120 });

                if (Form1_0.ObjectsStruc_0.GetObjects("PermanentTownPortal", true, new List<uint>()))
                {
                    Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                    
                    Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                    Form1_0.WaitDelay(100);
                }
                //Form1_0.Town_0.GoToWPArea(3, 8);
            }
            else
            {
                if (CurrentStep == 0)
                {
                    Form1_0.SetGameStatus("DOING PINDLESKIN");
                    Form1_0.Battle_0.CastDefense();
                    Form1_0.WaitDelay(15);

                    if ((Enums.Area) Form1_0.PlayerScan_0.levelNo == Enums.Area.NihlathaksTemple)
                    {
                        CurrentStep++;
                    }
                    else
                    {
                        Form1_0.Town_0.GoToTown();
                    }
                }

                if (CurrentStep == 1)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 10058, Y = 13236 });
                    CurrentStep++;
                }

                if (CurrentStep == 2)
                {
                    Form1_0.Potions_0.CanUseSkillForRegen = false;
                    Form1_0.SetGameStatus("KILLING PINDLESKIN");
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Pindleskin", false, 200, new List<long>()))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Pindleskin");
                        }
                        else
                        {
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                            Form1_0.Potions_0.CanUseSkillForRegen = true;

                            Form1_0.Town_0.UseLastTP = false;
                            ScriptDone = true;
                            return;
                            //Form1_0.LeaveGame(true);
                        }
                    }
                    else
                    {
                        Form1_0.method_1("Pindleskin not detected!", Color.Red);

                        //baal not detected...
                        Form1_0.ItemsStruc_0.GetItems(true);
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Pindleskin", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Pindleskin", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.Potions_0.CanUseSkillForRegen = true;

                        Form1_0.Town_0.UseLastTP = false;
                        ScriptDone = true;
                        return;
                        //Form1_0.LeaveGame(true);
                    }
                }
            }
        }
    }
}
