using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CompaniesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public IActionResult GetCompany(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompany(id, trackChanges: false);
        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyForCreationDto companyForCreationDto)
    {
        if (companyForCreationDto is null)
            return BadRequest("CompanyForCreationDto object is null");

        var createdCompany = _serviceManager.CompanyService.CreateCompany(companyForCreationDto);

        return CreatedAtRoute("CompanyById", new {id = createdCompany.Id}, createdCompany);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var companies = _serviceManager.CompanyService.GetByIds(ids, trackChanges: false);
        return Ok(companies);
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection
        ([FromBody] IEnumerable<CompanyForCreationDto> companyForCreationDtoCollection)
    {
        var result = _serviceManager.CompanyService.CreateCompanyCollection(companyForCreationDtoCollection);
        return CreatedAtRoute("CompanyCollection", new {result.ids}, result.companiesDto);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCompany(Guid id)
    {
        _serviceManager.CompanyService.DeleteCompany(id, trackChanges:false);
        return NoContent();
    }
}