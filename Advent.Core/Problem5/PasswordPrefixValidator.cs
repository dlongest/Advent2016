using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem5
{
    public class PasswordPrefixValidator : IPasswordValidator
    {
        private string prefix;

        public PasswordPrefixValidator(string requiredPrefix)
        {
            this.prefix = requiredPrefix;
        }

        public bool IsValid(string hexadecimalHash)
        {
            return hexadecimalHash.StartsWith(this.prefix);
        }
    }
}
