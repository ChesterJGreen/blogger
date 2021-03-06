using blogger.Models;
using blogger.Repositories;

namespace blogger.Services
{
    public class AccountService
    {
        private readonly AccountsRepository _repo;
        public AccountService(AccountsRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetProfileById(string id)
        {
            return _repo.GetById(id);
        }
        internal Account GetProfileByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        internal Account GetOrCreateProfile(Account userInfo)
        {
            Account profile = GetProfile(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }
        internal Account GetProfile(string id) 
        {
            return _repo.GetById(id);
        }

        internal Account Edit(Account editData, Account userInfo)
        {
            Account original = GetProfile(userInfo.Id);
            original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
            original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
            return _repo.Edit(original);
        }
    }
}