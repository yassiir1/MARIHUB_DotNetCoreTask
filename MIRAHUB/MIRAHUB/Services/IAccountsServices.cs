using MIRAHUB.Models;

namespace MIRAHUB.Services
{
    public interface IAccountsServices
    {
        public List<Accounts> GetAccounts();
        public Accounts GetAccountBy(int id);
        public string UpdateAccount(Accounts Account);
        public string AddAccount(Accounts Account);
        public string DeleteAccount(int id);
    }
}
