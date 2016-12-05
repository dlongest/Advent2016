using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem5
{
    public class PasswordFinder
    {
        private IPasswordValidator validator;
        private ISequenceGenerator<int> intGenerator;
        private int characterIndexInHash = 6;
        private int positionIndexInHash = 5;

        public PasswordFinder(IPasswordValidator validator, ISequenceGenerator<int> generator)
        {
            this.validator = validator;
            this.intGenerator = generator;
        }

        public string Find(string secretKey, int passwordLength)
        {
            var password = new Password(passwordLength);

            while (!password.IsComplete())
            {
                var nextPasswordCharacter = FindNextPasswordCharacter(secretKey);

                password = password.AddCharacter(nextPasswordCharacter);
            }

            return password.ToString();
        }


        private PasswordCharacter FindNextPasswordCharacter(string secretKey)
        {
            foreach (var number in this.intGenerator.Generate())
            {
                var hash = ComputeHexadecimalHash(secretKey, number);

                if (IsValid(hash))
                {
                    return new PasswordCharacter(hash);
                }
            }

            throw new ArgumentException("Could not find password character for this secretKey");
        }

        private bool IsValid(string hexadecimalHash)
        {
            return this.validator.IsValid(hexadecimalHash) &&
                    PasswordCharacter.IsValid(hexadecimalHash, this.characterIndexInHash, this.positionIndexInHash);
        }

     
        private string ComputeHexadecimalHash(string secretKey, int decimalNumber)
        {
            return Md5Hash.Compute(string.Format("{0}{1}", secretKey, decimalNumber), bytes => bytes.ToHexString());
        }      

        private class PasswordCharacter
        {
            public static bool IsValid(string hexadecimalHash, int characterIndex, int positionIndex)
            {
                if (!hexadecimalHash.CharAt(positionIndex).IsDigit())
                    return false;

                return true;
            }

            public PasswordCharacter(string hexadecimalHash)
            {
                Position = Int32.Parse(hexadecimalHash.CharAt(5));
                Character = hexadecimalHash[6];
                this.Hash = hexadecimalHash;
            }

            public char Character { get; set; }

            public int Position { get; set; }    
            
            public string Hash { get; set; }      
        }     
        
        private class Password
        {
            private char?[] password;

            public Password(int length)
            {
                this.password = new char?[length];
            }

            public Password AddCharacter(PasswordCharacter character)
            {
                if (character.Position >= this.password.Length)
                    return this;

                if (this.password[character.Position].HasValue)
                    return this;

                this.password[character.Position] = character.Character;
                return this;
            }

            public bool IsComplete()
            {
                return this.password.All(c => c.HasValue);
            }

            public override string ToString()
            {
                var p = this.password.Select(a => a.HasValue ? a.Value : '*');

                return new string(p.ToArray());
            }
        }
    }        
}
