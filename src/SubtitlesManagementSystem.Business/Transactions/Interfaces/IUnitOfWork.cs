using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Business.Transactions.Interfaces
{
    public interface IUnitOfWork
    {
        bool CommitSaveChanges();
    }
}
