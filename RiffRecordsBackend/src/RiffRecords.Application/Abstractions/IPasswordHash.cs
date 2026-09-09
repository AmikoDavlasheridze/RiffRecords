using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions
{
    public interface IPasswordHash
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
}
