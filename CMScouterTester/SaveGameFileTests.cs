using CMScouter.UI;
using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouterTester
{
    [TestClass]
    public class SaveGameFileTests
    {
        protected const string LiverpoolSave = @"C:\Install\Games\CM 0102\A Large Oldham EndOfSeason.sav";
        protected const string OldhamSave = @"C:\Install\Games\CM 0102\Oldham Cheating.sav";
        protected const string BurySave = @"C:\Install\Games\CM0102\Bury End Season 1 3.9.68.sav";
        protected const string BonnyriggSave = @"C:\Install\Games\CM0102\World Tour (AUS).sav";
        protected const string EstorilSave = @"C:\Install\Games\CM0102\Estoril.sav";
        static CMScouterUI cmsUI;

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            cmsUI = new CMScouterUI(EstorilSave);
        }

        [TestMethod]
        [DataRow("VAN DER")]
        public void TestSearchByPlayerSecondName(string playerName)
        {
            List<PlayerView> players = cmsUI.GetPlayersBySecondName(playerName);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0);
            Assert.IsTrue(players.All(p => p.SecondName.StartsWith(playerName, StringComparison.InvariantCultureIgnoreCase)));
        }

        //[DataRow(1233)] 
        //[DataRow(6898)] //Brown
        //[DataRow(5387)] //Lovren
        [DataRow(41005)] // Duarte
        //[DataRow(32377)] // Digne
        //[DataRow(29838)] // Sefil
        //[DataRow(2163)]
        //[DataRow(62797)]
        [TestMethod]
        public void TestSearchByPlayerId(int playerId1)
        {
            List<PlayerView> players = cmsUI.GetPlayerByPlayerId(new List<int>() { playerId1 });

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count == 1);
            Assert.IsTrue(players.Any(p => p.PlayerId == playerId1));
        }

        [DataRow("LIVERPOOL")]
        [TestMethod]
        public void TestSearchByClubName(string clubName)
        {
            ScoutingRequest request = new ScoutingRequest() { ClubId = cmsUI.GetClubByName(clubName)?.ClubId };
            List<PlayerView> players = cmsUI.GetScoutResults(request);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0);
            Assert.IsTrue(players.All(x => !string.IsNullOrEmpty(x.FirstName) && !string.IsNullOrEmpty(x.SecondName)));
            Assert.IsTrue(players.All(x => x.PlayerId > 0));
            Assert.IsTrue(players.All(x => !string.IsNullOrEmpty(x.ClubName)));
        }

        [TestMethod]
        [DataRow("HENRY")]
        public void TestSearchForUnemployedPlayer(string playerName)
        {
            List<PlayerView> players = cmsUI.GetPlayersBySecondName(playerName);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0);
            Assert.IsTrue(players.All(p => p.SecondName.StartsWith(playerName, StringComparison.InvariantCultureIgnoreCase)));
            Assert.IsTrue(players.Any(p => p.ClubName == string.Empty));
        }

        [TestMethod]
        [DataRow("SCOTLAND")]
        public void TestSearchByNationality(string nationality)
        {
            int? nationID = cmsUI.GetAllNations().FirstOrDefault(x => x.Name.Equals(nationality, StringComparison.InvariantCultureIgnoreCase))?.Id;
            Assert.IsNotNull(nationID.Value);
            
            ScoutingRequest request = new ScoutingRequest() { Nationality = nationID.Value };
            List<PlayerView> players = cmsUI.GetScoutResults(request);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0);
            Assert.IsTrue(players.All(p => p.Nationality.StartsWith(nationality, StringComparison.InvariantCultureIgnoreCase) || p.SecondaryNationality?.StartsWith(nationality, StringComparison.InvariantCultureIgnoreCase) == true));
        }
        
        [TestMethod]
        public void TestSearchForHighestGKs()
        {
            ScoutingRequest request = new ScoutingRequest() { PlayerType = PlayerPosition.GoalKeeper, NumberOfResults = 20 };
            List<PlayerView> players = cmsUI.GetScoutResults(request);
            
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0);
            Assert.IsTrue(players.All(p => p.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.GoalKeeper).BestRole.AbilityRating > 70));
            Assert.IsTrue(players.All(p => p.CurrentAbility > 80));
        }

        [TestMethod]
        public void TestHighestTacklers()
        {
            List<PlayerView> players = cmsUI.GetHighestIntrinsic(DP.Tackling, 30);
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count == 30);
            Assert.IsTrue(players.All(x => x.Attributes.Tackling > 128));
        }

        [TestMethod]
        public void TestHighestPassing()
        {
            List<PlayerView> players = cmsUI.GetHighestIntrinsic(DP.Passing, 30);
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count == 30);
            Assert.IsTrue(players.All(x => x.Attributes.Passing > 128));
        }

        [TestMethod]
        public void TestSearchForBestFreeTransfers()
        {
            ScoutingRequest request = new ScoutingRequest() { MaxValue = 0, NumberOfResults = 100 };
            List<PlayerView> players = cmsUI.GetScoutResults(request);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count >= 20);
        }

        [TestMethod]
        public void TestSearchForCentreBacks()
        {
            ScoutingRequest request = new ScoutingRequest() { PlayerType = PlayerPosition.CentreHalf, MaxValue = 2000000, NumberOfResults = 50, EUNationalityOnly = true };
            List<PlayerView> players = cmsUI.GetScoutResults(request);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count >= 20);
            Assert.IsTrue(players.All(p => p.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.CentreHalf).BestRole.AbilityRating > 50));
        }

        [TestMethod]
        public void TestSearchForAttackingMidfielders()
        {
            ScoutingRequest request = new ScoutingRequest() { PlayerType = PlayerPosition.AttackingMidfielder, MaxValue = 2000000, NumberOfResults = 50, EUNationalityOnly = true };
            List<PlayerView> players = cmsUI.GetScoutResults(request);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count >= 20);
            Assert.IsTrue(players.All(p => p.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.AttackingMidfielder).BestRole.AbilityRating > 50));
        }

        [TestMethod]
        [DataRow("RAMSES BECKER")]
        public void TestGoalkeeperRatings(string playerSurname)
        {
            List<PlayerView> players = cmsUI.GetPlayersBySecondName(playerSurname);
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count >= 1);

            var keeper = players.OrderByDescending(p => p.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.GoalKeeper).BestRole.AbilityRating).First();

            Assert.IsNotNull(keeper);
            Assert.IsTrue(keeper.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.GoalKeeper).BestRole.AbilityRating > 70);
        }

        [TestMethod]
        [DataRow("OLDHAM", "BROWN")]
        [DataRow("LIVERPOOL", "VAN DIJK")]
        public void TestCentreHalfRatings(string clubName, string playerSurname)
        {
            ScoutingRequest request = new ScoutingRequest() { ClubId = cmsUI.GetClubByName(clubName)?.ClubId };
            List<PlayerView> players = cmsUI.GetScoutResults(request);
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0 && players.Count <= 50);

            var player = players.First(f => f.SecondName.Equals(playerSurname, StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(player);
            //Assert.IsTrue(player.ScoutRatings.CentreHalf.BestRole().AbilityRating > 50 && player.ScoutRatings.CentreHalf.BestRole().AbilityRating < 60);
        }

        [TestMethod]
        [DataRow("LIVERPOOL", "FIRMINO")]
        public void TestAttackingMidfielderRatings(string clubName, string playerSurname)
        {
            ScoutingRequest request = new ScoutingRequest() { ClubId = cmsUI.GetClubByName(clubName)?.ClubId };
            List<PlayerView> players = cmsUI.GetScoutResults(request);
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0 && players.Count <= 50);

            var player = players.First(f => f.SecondName.Equals(playerSurname, StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(player);
            Assert.IsTrue(player.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.AttackingMidfielder).BestRole.AbilityRating > 70);
        }

        [TestMethod]
        [DataRow("LIVERPOOL2", "SALAH")]
        public void TestStrikerRatings(string clubName, string playerSurname)
        {
            ScoutingRequest request = new ScoutingRequest() { ClubId = cmsUI.GetClubByName(clubName)?.ClubId };
            List<PlayerView> players = cmsUI.GetScoutResults(request);
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0 && players.Count <= 50);

            var player = players.First(f => f.SecondName.Equals(playerSurname, StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(player);
            Assert.IsTrue(player.ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.CentreForward).BestRole.AbilityRating > 70);
        }

        [TestMethod]
        public void TestDiv3Strikers()
        {
            List<Club_Comp> allComps = cmsUI.GetAllClubCompetitions();
            var Div3 = allComps.FirstOrDefault(x => x.LongName.Equals("English Third Division", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsNotNull(Div3);

            ScoutingRequest request = new ScoutingRequest() { NumberOfResults = 30, PlayerType = PlayerPosition.CentreForward, PlaysInDivision = Div3.Id };
            var players = cmsUI.GetScoutResults(request);

            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 0);
            Assert.IsTrue(players[0].ScoutRatings.PositionRatings.First(x => x.SetPosition == PlayerPosition.CentreForward).Rating > 50);
        }
    }
}
