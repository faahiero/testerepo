using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    CompanyDto GetCompany(Guid companyId, bool trackChanges);
    CompanyDto CreateCompany(CompanyForCreationDto companyForCreationDto);
    IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

    (IEnumerable<CompanyDto> companiesDto, string ids) CreateCompanyCollection
        (IEnumerable<CompanyForCreationDto> companyForCreationDtoCollection);

    void DeleteCompany(Guid companyId, bool trackChanges);
}