
using F1GrpcServer;
using Formula1ApiConnection.Repositories;
using Grpc.Core;
using Models.Models;

namespace Formula1ApiConnection.GrpcServices;

public class F1GrpcService : F1Grpc.F1GrpcBase
{
    public override async Task<DriversChampionshipsResponse> GetDriversChampionship(DriversChampionshipsRequest request, 
        ServerCallContext context)
    {
        var driversResponseModel = request.HasYear 
            ? await F1ApiReader.GetDriversChampionshipByYearAsync(request.Year)
            : await F1ApiReader.GetCurrentDriversChampionshipAsync();

        List<DriversChampionships> drivers = new();

        if (driversResponseModel != null)
        {
            drivers.AddRange(driversResponseModel.DriversChampionships.Select(d=>
                new DriversChampionships()
                {
                    ClassificationId = d.ClassificationId,
                    Points = d.Points,
                    Position = d.Position,
                    Wins = d.Wins ?? 0,
                    TeamId = d.TeamId,
                    DriverId = d.DriverId,
                    DriverInfo = new Driver()
                    {
                        Name = d.Driver.Name,
                        Nationality = d.Driver.Nationality,
                        Number = d.Driver.Number,
                        Surname = d.Driver.Surname,
                        Url = d.Driver.Url
                    }
                }));
        }
        
        DriversChampionshipsResponse response = new DriversChampionshipsResponse()
        {
            Drivers = { drivers }
        };

        return await Task.FromResult(response);

    }

    public override async Task<ConstructorsChampionshipResponse> GetConstructorsChampionship(ConstructorsChampionshipRequest request, 
        ServerCallContext context)
    {
        var constructorsResponseModel = request.HasYear
            ? await F1ApiReader.GetConstructorsChampionshipByYearAsync(request.Year) 
            : await F1ApiReader.GetCurrentConstructorsChampionshipAsync();
        
        List<ConstructorsChampionship> constructorsChampionships = new();
            
        if (constructorsResponseModel != null)
        {
            constructorsChampionships.AddRange(
                constructorsResponseModel.ConstructorsChampionship.
                    Select(constructors => new ConstructorsChampionship()
            {
                TeamId = constructors.TeamId,
                Points = constructors.Points,
                Position = constructors.Position,
                Wins = constructors.Wins ?? 0,
                ClassificationId = constructors.ClassificationId,
                TeamInfo = new Team()
                {
                    Name = constructors.TeamResponse.TeamName,
                    Url = constructors.TeamResponse.Url,
                    Country = constructors.TeamResponse.TeamNationality,
                }
            }));
        }

        ConstructorsChampionshipResponse response = new ConstructorsChampionshipResponse()
        {
            Constructors = { constructorsChampionships }
        };

        return await Task.FromResult(response);
        
    }
}