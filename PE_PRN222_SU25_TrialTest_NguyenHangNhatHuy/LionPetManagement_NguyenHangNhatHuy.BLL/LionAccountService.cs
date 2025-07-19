using LionPetManagement_NguyenHangNhatHuy.DAL.UnitOfWorks;
using LionPetManagement_NguyenHangNhatHuy.DAL.Models;

namespace LionPetManagement_NguyenHangNhatHuy.BLL
{
	public class LionAccountService
	{
		private readonly UnitOfWork _unitOfWork;
		public LionAccountService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public LionAccount Login(string email, string password)
		{
			return _unitOfWork.GetRepository<LionAccount>().FindByCondition(a => a.Email == email && a.Password == password);
		}

	}
}
