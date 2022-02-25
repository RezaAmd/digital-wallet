using Application.Dao;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services.WalletService
{
    public class WalletService
    {
        #region Dependency Injection
        private readonly IWalletDao walletDao;
        private readonly ITransferDao transferDao;

        public WalletService(IWalletDao _walletDao,
            ITransferDao _transferDao)
        {
            walletDao = _walletDao;
            transferDao = _transferDao;
        }
        #endregion


    }
}