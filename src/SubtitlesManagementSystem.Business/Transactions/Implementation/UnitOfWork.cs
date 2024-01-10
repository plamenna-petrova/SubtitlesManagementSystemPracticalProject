using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;

namespace SubtitlesManagementSystem.Business.Transactions.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly ILogger _logger;

        public UnitOfWork(
            ApplicationDbContext applicationDbContext,
            ILogger<UnitOfWork> logger
        )
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public bool CommitSaveChanges()
        {
            try
            {
                int rowsAffected = _applicationDbContext.SaveChanges();

                if (rowsAffected > 0)
                {
                    return true;
                }

                return false;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError("Exception: " + dbUpdateException.Message +
                            "\n" + "Inner Exception :" +
                    dbUpdateException.InnerException.Message ?? "");

                return false;
            }
        }
    }
}
