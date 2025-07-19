using LionPetManagement_NguyenHangNhatHuy.DAL.UnitOfWorks;
using LionPetManagement_NguyenHangNhatHuy.DAL.Models;

namespace LionPetManagement_NguyenHangNhatHuy.BLL
{
	public class LionTypeService
	{
		private readonly UnitOfWork _unitOfWork;
		public LionTypeService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<LionType> GetLionType()
		{
			return _unitOfWork.GetRepository<LionType>().GetAll();
		}
	}
}
