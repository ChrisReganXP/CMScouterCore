using CMScouter.DataClasses;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouterTester
{
    [TestClass]
    public class MaskerTests
    {
        MadScientist_MatchMasker masker = new MadScientist_MatchMasker();

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {

        }

        [TestMethod]
        public void TestMasker()
        {
            PlayerData player = new PlayerData();
            player.Centre = 20;
            player.DF = 20;
            player.Positioning = 130;
            player.CurrentAbility = 125;

            CycleTestsUpAndDown(player, PlayerPosition.CentreHalf, PlayerPosition.CentreHalf);
        }

        private void CycleTestsUpAndDown(PlayerData player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            short initial_ability = player.CurrentAbility;
            byte adjusted = 0;
            for (int i = 0; i <= 5; i++)
            {
                if (player.CurrentAbility > 190)
                {
                    break;
                }

                player.CurrentAbility += (short)(i * 10);
                byte new_adjusted = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.CentreHalf, PlayerPosition.CentreHalf, player.Positioning);

                if (i == 0)
                {
                    Assert.AreEqual(new_adjusted, SimpleMasking(player.Positioning, player.CurrentAbility));
                }
                else
                {
                    Assert.IsTrue(new_adjusted > adjusted);
                }

                adjusted = new_adjusted;
            }

            player.CurrentAbility = initial_ability;
        }

        private byte SimpleMasking(byte val, short currentAbility)
        {
            decimal valueAspect = (val - 128) / 5;
            decimal abilityAspect = currentAbility / 20;

            return (byte)(valueAspect + abilityAspect + 10);
        }
    }
}
