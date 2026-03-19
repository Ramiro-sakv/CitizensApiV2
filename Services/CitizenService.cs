using CitizensApiV2.DTOs;
using CitizensApiV2.Models;
using CitizensApiV2.Repositories;

namespace CitizensApiV2.Services;

public class CitizenService
{
    private readonly CitizenRepository _repo;
    private readonly ObjectService _objectService;
    private readonly ILogger<CitizenService> _logger;

    public CitizenService(
        CitizenRepository repo,
        ObjectService objectService,
        ILogger<CitizenService> logger)
    {
        _repo = repo;
        _objectService = objectService;
        _logger = logger;
    }

    public List<Citizen> GetAll()
    {
        return _repo.GetAll();
    }

    public Citizen GetByCI(string ci)
    {
        var citizen = _repo.GetAll().FirstOrDefault(c => c.CI == ci);

        if (citizen == null)
        {
            throw new Exception("Citizen not found");
        }

        return citizen;
    }

    public async Task<Citizen> Create(CreateCitizenDto dto)
    {
        var citizens = _repo.GetAll();

        if (citizens.Any(c => c.CI == dto.CI))
        {
            throw new Exception("Citizen with this CI already exists");
        }

        var bloodGroups = new[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" };
        var random = new Random();

        var citizen = new Citizen
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CI = dto.CI,
            BloodGroup = bloodGroups[random.Next(bloodGroups.Length)],
            PersonalAsset = await _objectService.GetRandomAsset()
        };

        citizens.Add(citizen);
        _repo.SaveAll(citizens);

        _logger.LogInformation("Citizen created");

        return citizen;
    }

    public Citizen Update(string ci, UpdateCitizenDto dto)
    {
        var citizens = _repo.GetAll();
        var citizen = citizens.FirstOrDefault(c => c.CI == ci);

        if (citizen == null)
        {
            throw new Exception("Citizen not found");
        }

        citizen.FirstName = dto.FirstName;
        citizen.LastName = dto.LastName;

        _repo.SaveAll(citizens);

        _logger.LogInformation("Citizen updated");

        return citizen;
    }

    public void Delete(string ci)
    {
        var citizens = _repo.GetAll();
        var citizen = citizens.FirstOrDefault(c => c.CI == ci);

        if (citizen == null)
        {
            throw new Exception("Citizen not found");
        }

        citizens.Remove(citizen);
        _repo.SaveAll(citizens);

        _logger.LogInformation("Citizen deleted");
    }
}