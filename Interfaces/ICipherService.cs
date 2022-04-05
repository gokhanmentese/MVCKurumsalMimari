using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
   public interface ICipherService
    {
        string Encrypt(string cipherText);
        string Decrypt(string cipherText);
    }
}
