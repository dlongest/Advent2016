using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem4
{
    public class ShiftDecryptor : IRoomNameDecryptor
    {
        private static IDictionary<char, char> shiftCharacterMap;

        static ShiftDecryptor()
        {
            shiftCharacterMap = Enumerable.Range(97, 25)
                                          .Select(a => new { Character = (char)a, NewCharacter = (char)(a + 1) })
                                          .ToDictionary(a => a.Character, a => a.NewCharacter);

            shiftCharacterMap['z'] = 'a';
            shiftCharacterMap['-'] = ' ';
            shiftCharacterMap[' '] = ' ';
        }


        public string Decrypt(RoomName name)
        {
            var newName = name.EncryptedName;

            foreach (int i in Enumerable.Range(0, name.SectorID))
            {
                newName = ShiftToNewName(newName);
            }

            return newName;
        }

        private string ShiftToNewName(string name)
        {
            return new string(name.Select(c => Shift(c)).ToArray());
        }

        private char Shift(char c)
        {
            if (shiftCharacterMap.ContainsKey(c))
                return shiftCharacterMap[c];

            throw new ArgumentException("Unable to shift character");
        }
    }
}
