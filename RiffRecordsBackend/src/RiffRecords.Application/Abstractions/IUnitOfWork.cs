using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
