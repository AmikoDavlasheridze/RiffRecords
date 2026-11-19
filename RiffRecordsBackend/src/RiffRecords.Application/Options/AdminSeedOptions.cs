using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Options
{
    public sealed class AdminSeedOptions
    {
        public const string SectionName = "AdminSeed";

        public string Email { get; init; } = "";
        public string Username { get; init; } = "";
        public string Password { get; init; } = "";
    }
}
