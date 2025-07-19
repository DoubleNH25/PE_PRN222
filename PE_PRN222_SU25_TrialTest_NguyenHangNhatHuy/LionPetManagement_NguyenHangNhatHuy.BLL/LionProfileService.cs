using LionPetManagement_NguyenHangNhatHuy.DAL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_NguyenHangNhatHuy.DAL.Models;
namespace LionPetManagement_NguyenHangNhatHuy.BLL
{
	public class LionProfileService
	{
		private readonly UnitOfWork _unitOfWork;
		public LionProfileService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<LionProfile> GetList(int pageNumber, int pageSize, out int totalItems, string? lionName, string? lionTypeName, string? weight)
		{
			var query = _unitOfWork.GetRepository<LionProfile>().GetFiltered(null, null);

			if (!string.IsNullOrEmpty(lionName))
			{
				query = query.Where(m => m.LionName.Contains(lionName));
			}
			if (!string.IsNullOrEmpty(lionTypeName))
			{
				query = query.Where(m => m.LionType.LionTypeName.Contains(lionTypeName));
			}
			if (!string.IsNullOrEmpty(weight))
			{
				if (int.TryParse(weight, out int weights))
				{
					query = query.Where(m => m.Weight == weights);
				}
				else
				{
					query = query.Where(m => m.Weight.ToString().Contains(weight));
				}
			}

			totalItems = query.Count();

			return query
					.Skip((pageNumber - 1) * pageSize)
					.Take(pageSize)
					.Include(m => m.LionType)
					.ToList();
		}

		public LionProfile GetById(string id) //string id
		{
			return _unitOfWork.GetRepository<LionProfile>().GetById(id, m => m.LionType);
		}
		public LionProfile GetIntById(int id) //string id
		{
			return _unitOfWork.GetRepository<LionProfile>().GetById(id, m => m.LionType);
		}
		public void Add(LionProfile entity)
		{
			_unitOfWork.GetRepository<LionProfile>().Add(entity);
			_unitOfWork.Save();
		}
		public void Delete(LionProfile entity)
		{
			_unitOfWork.GetRepository<LionProfile>().Delete(entity);
			_unitOfWork.Save();
		}
		public void Update(LionProfile entity)
		{
			_unitOfWork.GetRepository<LionProfile>().Update(entity);
			_unitOfWork.Save();
		}
	}
}
