using CMScouter.UI.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;
using CMScouterFunctions;
using System.Linq;
using System.Dynamic;

namespace CMScouter.UI
{
    internal class ShortlistHelper
    {
        private const int MaxItemsOnShortlist = 200;

        internal static byte[] GetShortlistBytes(List<PlayerShortlistEntry> shortlist)
        {
            shortlist = shortlist.Take(MaxItemsOnShortlist).ToList();

            List<byte> shortlistBytes = GetByteListForShortlistFileStart();
            shortlistBytes.AddRange(new List<byte>() { (byte)shortlist.Count, 0, 0, 0 });

            foreach (var player in shortlist) 
            {
                List<byte> byteList = ByteHandler.GetBytesForInt(player.FirstNameId).ToList();
                byteList.AddRange(ByteHandler.GetBytesForInt(player.SecondNameId).ToList());
                byteList.AddRange(ByteHandler.GetBytesForInt(player.CommonNameId).ToList());
                byteList.AddRange(ByteHandler.GetBytesForInt(player.StaffId).ToList());
                byteList.AddRange(ByteHandler.GetBytesForDate(player.DOB).ToList());
                byteList.AddRange(new List<byte>() { 0, 0, 0 }); // don't know
                byteList.AddRange(new List<byte>() { 215, 0, 209, 7 }); // date added to shortlist
                
                // other unknown data
                for (int i = 0; i < 16; i++)
                {
                    byteList.Add(0);
                }

                // FF as divider
                byteList.Add(255);

                shortlistBytes.AddRange(byteList);
            }

            shortlistBytes.AddRange(GetByteListForShortlistFileEnd());

            return shortlistBytes.ToArray();
        }

        private static List<byte> GetByteListForShortlistFileStart()
        {
            // stuff at the top of my file
            List<byte> FileIntro = new List<byte>() { 158, 74, 2, 0, 1, 1 };

            List<byte> FileDescriptor = new List<byte> { 83/*S*/, 104/*h*/, 111/*o*/, 114/*r*/, 116/*t*/, 32/* */, 76/*L*/, 105/*i*/, 115/*s*/, 116/*t*/};
            FileDescriptor.AddRange(Enumerable.Range(0, 100 - FileDescriptor.Count).Select(i => (byte)0).ToList());

            //List<byte> CreatorName = new List<byte> { 67/*C*/, 77/*M*/, 83/*S*/, 99/*c*/, 111/*o*/, 117/*u*/, 116/*t*/, 101/*e*/, 114/*r*/};
            List<byte> CreatorName = new List<byte> { 67/*C*/, 104/*h*/, 114/*r*/, 105/*i*/, 115/*s*/, 32/* */, 82/*R*/, 101/*e*/, 103/*g*/, 97/*a*/, 110/*n*/};
            CreatorName.AddRange(Enumerable.Range(0, 100 - CreatorName.Count).Select(i => (byte)0).ToList());

            var fullOutput = FileIntro;
            fullOutput.AddRange(FileDescriptor);
            fullOutput.Add(0); // divider
            fullOutput.AddRange(CreatorName);
            fullOutput.AddRange(Enumerable.Range(0, 359 - fullOutput.Count).Select(i => (byte)0).ToList()); // blank spaces 
            fullOutput.AddRange(new List<byte>() { 79, 49, 0, 0, 250, 0, 0, 0 });

            return fullOutput;
        }

        private static List<byte> GetByteListForShortlistFileEnd()
        {
            // no idea what any of this is
            var FileOutro = new List<byte>() { 129, 0, 20, 20, 132, 168, 170, 68, 16 };
            FileOutro.AddRange(Enumerable.Range(0, 9).Select(i => (byte)0).ToList());
            FileOutro.Add(128);
            FileOutro.AddRange(Enumerable.Range(0, 5).Select(i => (byte)0).ToList());
            FileOutro.AddRange(Enumerable.Range(0, 5).Select(i => (byte)255).ToList());
            FileOutro.Add(1);
            FileOutro.AddRange(Enumerable.Range(0, 5).Select(i => (byte)255).ToList());
            FileOutro.Add(0); 
            FileOutro.Add(5);
            FileOutro.AddRange(Enumerable.Range(0, 31).Select(i => (byte)1).ToList());
            FileOutro.AddRange(Enumerable.Range(0, 31).Select(i => (byte)20).ToList());
            FileOutro.Add(0);
            FileOutro.Add(0);
            FileOutro.AddRange(Enumerable.Range(0, 12).Select(i => (byte)1).ToList());
            FileOutro.AddRange(Enumerable.Range(0, 12).Select(i => (byte)20).ToList());
            FileOutro.AddRange(Enumerable.Range(0, 36).Select(i => (byte)0).ToList());
            FileOutro.AddRange(new List<byte>() { 129, 0, 0, 0, 4, 0, 0, 0, 255, 255, 0, 0, 0, 0, 1, 0, 1, 255, 255, 255, 255, 242, 255, 255, 242 });

            return FileOutro;
        }
    }
}
