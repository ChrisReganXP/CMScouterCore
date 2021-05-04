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
    public class MadScientist_MaskerTests
    {
        MadScientist_MatchMasker masker = new MadScientist_MatchMasker();

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {

        }

        [TestMethod]
        public void BasicMaskerTests()
        {
            // Player Ability 90
            decimal rating = masker.GetIntrinsicBasicMask(50, 90);
            Assert.IsTrue(rating == 1);

            rating = masker.GetIntrinsicBasicMask(70, 90);
            Assert.IsTrue(rating == 3);

            rating = masker.GetIntrinsicBasicMask(90, 90);
            Assert.IsTrue(rating == 7);

            rating = masker.GetIntrinsicBasicMask(110, 90);
            Assert.IsTrue(rating == 11);

            rating = masker.GetIntrinsicBasicMask(130, 90);
            Assert.IsTrue(rating == 15);

            rating = masker.GetIntrinsicBasicMask(150, 90);
            Assert.IsTrue(rating == 19);

            rating = masker.GetIntrinsicBasicMask(170, 90);
            Assert.IsTrue(rating == 23);


            // Player Ability 130
            rating = masker.GetIntrinsicBasicMask(70, 130);
            Assert.IsTrue(rating == 5);

            rating = masker.GetIntrinsicBasicMask(90, 130);
            Assert.IsTrue(rating == 9);

            rating = masker.GetIntrinsicBasicMask(110, 130);
            Assert.IsTrue(rating == 13);

            rating = masker.GetIntrinsicBasicMask(130, 130);
            Assert.IsTrue(rating == 17);

            rating = masker.GetIntrinsicBasicMask(150, 130);
            Assert.IsTrue(rating == 21);

            rating = masker.GetIntrinsicBasicMask(170, 130);
            Assert.IsTrue(rating == 25);
        }

        [TestMethod]
        public void CycleMaskerTest()
        {
            PlayerData player = new PlayerData();
            player.Centre = 20;
            player.DF = 20;
            player.Positioning = 130;
            player.CurrentAbility = 40;

            CycleTestCurrentAbilityUp(player, PlayerPosition.CentreHalf, PlayerPosition.CentreHalf);
        }

        [TestMethod]
        public void DetailedMaskerTests()
        {
            DetailedMaskerTests_RightBackTests();
            DetailedMaskerTests_RightBackMovingToWingBack();
            DetailedMaskerTests_CentreMidfield_OutOfPosition_RightBack();

            DetailedMaskerTests_LeftBack_OutOfPosition_CentreForward();
        }

        private void DetailedMaskerTests_RightBackTests()
        {
            PlayerData player = new PlayerData();
            player.Right = 20;
            player.DF = 20;
            player.DM = 1;
            player.WingBack = 1;

            player.Positioning = 128;
            player.CurrentAbility = 100;
            player.Versatility = 20;

            var rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightBack, player.Positioning);
            Assert.IsTrue(rating == 15);

            player.Versatility = 1;
            rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightBack, player.Positioning);
            Assert.IsTrue(rating == 15);
        }

        private void DetailedMaskerTests_RightBackMovingToWingBack()
        {
            PlayerData player = new PlayerData();
            player.Right = 20;
            player.DF = 20;
            player.DM = 1;
            player.WingBack = 1; 
            
            player.Positioning = 128;
            player.CurrentAbility = 100;
            player.Versatility = 20;

            var rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightWingBack, player.Positioning);
            Assert.IsTrue(rating == 14);

            player.Versatility = 10;
            rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightWingBack, player.Positioning);
            Assert.IsTrue(rating == 13);
        }

        private void DetailedMaskerTests_CentreMidfield_OutOfPosition_RightBack()
        {
            PlayerData player = new PlayerData();
            player.Centre = 20;
            player.Right = 1;
            player.MF = 20;
            player.DF = 1;
            player.DM = 1;
            player.WingBack = 1;

            player.Positioning = 128;
            player.CurrentAbility = 100;
            player.Versatility = 20;

            var rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightBack, player.Positioning);
            Assert.IsTrue(rating == 9);

            rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightWingBack, player.Positioning);
            Assert.IsTrue(rating == 8);

            player.Versatility = 10;
            rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.RightBack, PlayerPosition.RightWingBack, player.Positioning);
            Assert.IsTrue(rating == 1);
        }

        private void DetailedMaskerTests_LeftBack_OutOfPosition_CentreForward()
        {
            PlayerData player = new PlayerData();
            player.Centre = 1;
            player.Left = 20;
            player.MF = 1;
            player.DF = 20;
            player.DM = 1;
            player.AM = 1;
            player.ST = 1;
            player.WingBack = 1;

            player.OffTheBall = 128;
            player.CurrentAbility = 100;
            player.Versatility = 20;

            var rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.OffTheBall, PlayerPosition.CentreForward, PlayerPosition.CentreForward, player.OffTheBall);
            Assert.IsTrue(rating == 10);

            player.Versatility = 8;
            rating = masker.GetIntrinsicMask(player, CMScouter.UI.DP.OffTheBall, PlayerPosition.CentreForward, PlayerPosition.CentreForward, player.OffTheBall);
            Assert.IsTrue(rating == 1);
        }

        private void CycleTestCurrentAbilityUp(PlayerData player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            short initial_ability = player.CurrentAbility;
            decimal adjusted = 0;
            for (int i = 0; i <= 15; i++)
            {
                if (player.CurrentAbility > 190)
                {
                    break;
                }

                player.CurrentAbility += (short)10;
                decimal new_adjusted = masker.GetIntrinsicMask(player, CMScouter.UI.DP.Positioning, PlayerPosition.CentreHalf, PlayerPosition.CentreHalf, player.Positioning);

                if (i == 0)
                {
                    Assert.AreEqual(new_adjusted, SimpleMasking(player.Positioning, player.CurrentAbility));
                }
                else
                {
                    Assert.IsTrue(new_adjusted >= adjusted);
                }

                adjusted = new_adjusted;
            }

            player.CurrentAbility = initial_ability;
        }

        private byte SimpleMasking(byte val, short currentAbility)
        {
            decimal valueAspect = (decimal)(val - 128) / 5;
            decimal abilityAspect = (decimal)currentAbility / 20;

            return (byte)Math.Min(99, Math.Max(1, Math.Round(valueAspect + abilityAspect + 10)));
        }
    }
}
