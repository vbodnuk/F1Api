
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
        List<DriversChampionshipsModel> drivers;
        
        if (request.HasYear)
        {
            var apiResult = await F1ApiReader.GetDriversChampionshipByYear(request.Year);

            drivers = apiResult;
        }
        else
        {
            var apiResult = await F1ApiReader.GetCurrentDriversChampionship();

            drivers = apiResult;
        }

        var result = drivers.Select(d => new DriverChampionships()
        {
            DriverId = d.DriverId,
            ClassificationId = d.ClassificationId,
            Points = d.Points,
            Position = d.Position,
            Wins = d.Wins,
            TeamId = d.TeamId
        });
        
        DriversChampionshipsResponse response = new DriversChampionshipsResponse()
        {
            Drivers = { result }
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
                Wins = (int)constructors.Wins!,
                ClassificationId = constructors.ClassificationId
            }));
        }

        ConstructorsChampionshipResponse response = new ConstructorsChampionshipResponse()
        {
            Constructors = { constructorsChampionships }
        };

        return await Task.FromResult(response);
        
    }
}