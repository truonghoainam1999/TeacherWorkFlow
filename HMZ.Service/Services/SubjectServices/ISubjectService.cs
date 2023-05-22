using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;

namespace HMZ.Service.Services.SubjectServices
{
	public interface ISubjectService : IBaseService<SubjectQuery, SubjectView, SubjectFilter>
	{
		Task<DataResult<SubjectView>> GetByCodeAsync(string subjectCode);
	}
}
