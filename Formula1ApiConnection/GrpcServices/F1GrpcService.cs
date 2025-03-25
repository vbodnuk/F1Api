
using F1GrpcServer;
using Formula1ApiConnection.Models;
using Formula1ApiConnection.Repositories;
using Grpc.Core;

namespace Formula1ApiConnection.GrpcServices;

public class F1GrpcService : F1Grpc.F1GrpcBase
{
    public override async Task<DriversChampionshipsResponse> GetDriversChampionship(DriversChampionshipsRequest request, 
        ServerCallContext context)
    {
        DriversResponseModel? driversResponseModel = null;
        
        if (request.HasYear)
        {
            var apiResult = await F1ApiReader.GetDriversChampionshipByYear(request.Year);

            driversResponseModel = apiResult;
        }
        else
        {
            var apiResult = await F1ApiReader.GetCurrentDriversChampionship();

            driversResponseModel = apiResult;
        }

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
        ConstructorsResponseModel? constructorsResponseModel = null;
        
        if (request.HasYear)
        {
            var apiResult = await F1ApiReader.GetConstructorsChampionshipByYear(request.Year);
            constructorsResponseModel = apiResult;
        }
        else
        {
            var apiResult = await F1ApiReader.GetCurrentConstructorsChampionship();
            constructorsResponseModel = apiResult;
        }
        
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
                    Name = constructors.Team.TeamName,
                    Url = constructors.Team.Url,
                    Country = constructors.Team.TeamNationality,
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