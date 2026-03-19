using CitizensApiV2.Models;

namespace CitizensApiV2.Repositories;

public class CitizenRepository
{
    private readonly string _filePath;
    private readonly ILogger<CitizenRepository> _logger;

    public CitizenRepository(IConfiguration config, ILogger<CitizenRepository> logger)
    {
        _logger = logger;
        _filePath = config["AppSettings:CitizensFilePath"] ?? "Data/citizens.csv";

        var directory = Path.GetDirectoryName(_filePath);

        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    public List<Citizen> GetAll()
    {
        try
        {
            var lines = File.ReadAllLines(_filePath);

            return lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var values = line.Split(',');

                    return new Citizen
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        CI = values[2],
                        BloodGroup = values[3],
                        PersonalAsset = values[4]
                    };
                })
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading citizens file");
            return new List<Citizen>();
        }
    }

    public void SaveAll(List<Citizen> citizens)
    {
        try
        {
            var lines = citizens.Select(c =>
                string.Join(",", c.FirstName, c.LastName, c.CI, c.BloodGroup, c.PersonalAsset));

            File.WriteAllLines(_filePath, lines);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error writing citizens file");
            throw;
        }
    }
}