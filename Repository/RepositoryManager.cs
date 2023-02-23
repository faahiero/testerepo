using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICompanyRepository> _companyRepositoy;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _companyRepositoy = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
    }

    public ICompanyRepository Company => _companyRepositoy.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();

}