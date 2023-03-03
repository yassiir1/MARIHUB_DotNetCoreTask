using Microsoft.EntityFrameworkCore;
using MIRAHUB.Models;

namespace MIRAHUB.Services
{
    public class AccountsServices : IAccountsServices
    {
        private readonly MIRAHUBDb _context;
        public AccountsServices(MIRAHUBDb context)
        {
            _context = context;
        }

        public string AddAccount(Accounts Account)
        {
            var Status = "";
            try
            {
                _context.Accounts.Add(Account);
                _context.SaveChanges();
                Status = "Success";
            }
            catch(Exception ex)
            {
                Status = "Failed";
            }
            return Status;
        }

        public string DeleteAccount(int id)
        {
            var Status = "";
            var accounts =  _context.Accounts.Find(id);
            if (accounts == null)
                return "Not Found";
            try
            {
                _context.Accounts.Remove(accounts);
                _context.SaveChanges();
                Status = "Success";
            }
            catch(Exception ex)
            {
                Status = "Failed";
            }
            return Status;

        }

        public Accounts GetAccountBy(int id)
        {
            var data = _context.Accounts.Find(id);
            return data;
        }

        public List<Accounts> GetAccounts()
        {
            var data =  _context.Accounts.ToList();
            return data;
        }

        public string UpdateAccount(Accounts Account)
        {
            _context.Entry(Account).State = EntityState.Modified;
            var Status = "";
            try
            {
                 _context.SaveChangesAsync();
                Status = "Success";

            }
            catch (Exception ex)
            {
                Status = "Failes";
            }
            return Status;
        }
    }
}
