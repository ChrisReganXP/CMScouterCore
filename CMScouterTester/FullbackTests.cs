using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMScouter.UI;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System.Linq;

namespace CMScouterTester
{
    [TestClass]
    public class FullbackTests
    {
        static IPlayerRater rater;

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            rater = new GroupedAttributeRater(new MadScientist_MatchMasker());
        }

        [TestMethod]
        public void Lauren()
        {
            PlayerData attributes = new PlayerData();
            attributes.DF = 20;
            attributes.Right = 20;
            attributes.CurrentAbility = 144;

            attributes.Acceleration = 17;
            attributes.Agility = 16;
            attributes.Balance = 12;
            attributes.InjuryProneness = 8;
            attributes.Jumping = 12;
            attributes.NaturalFitness = 18;
            attributes.Pace = 17;
            attributes.Stamina = 19;
            attributes.Strength = 18;

            attributes.Anticipation = 138;
            attributes.Creativity = 98;
            attributes.Crossing = 108;
            attributes.Decisions = 114;
            attributes.Dribbling = 108;
            attributes.Finishing = 110;
            attributes.Heading = 95;
            attributes.LongShots = 109;
            attributes.Marking = 124;
            attributes.OffTheBall = 124;
            attributes.Passing = 113;
            attributes.Positioning = 124;
            attributes.Tackling = 118;
            attributes.Technique = 18;

            attributes.Aggression = 14;
            attributes.Bravery = 12;
            attributes.Consistency = 16;
            attributes.Dirtiness = 9;
            attributes.Flair = 8;
            attributes.ImportantMatches = 14;
            attributes.Influence = 12;
            attributes.Teamwork = 17;
            attributes.Versatility = 20;
            attributes.WorkRate = 16;

            Player Lauren = new Player(attributes, new Staff() { Determination = 16 }, null); ;
            var scouting = rater.GetRatings(Lauren);
            var fullBackRatings = scouting.PositionRatings.First(x => x.SetPosition == CMScouter.DataClasses.PlayerPosition.RightBack);
            Assert.IsTrue(fullBackRatings.BestRole.Role == CMScouter.DataClasses.Roles.DFB);
            Assert.IsTrue(fullBackRatings.BestRole.AbilityRating >= 85 && fullBackRatings.BestRole.AbilityRating <= 90);
        }
    }
}
