# F1Api  

F1Api is a service for fetching Formula 1 data from an external API.  
It provides an easy way to access necessary information and includes gRPC for communication with external services.  

## Features  
- Retrieves data from an external API.  
- Uses gRPC for service-to-service communication.  

## Technologies  
- .NET 8  
- gRPC  
- Serilog for logging  
- HttpClient for API requests  

## gRPC  

The service uses gRPC for communication with external clients. To generate a gRPC client, use the following command:  

**Note:** Adjust paths if necessary. `./Protos` is the default folder for `.proto` files, and `./Generated` is the output folder for generated C# files.  

```sh
protoc --proto_path=./Protos --csharp_out=./Generated \
--grpc_out=./Generated --plugin=protoc-gen-grpc=grpc_csharp_plugin \
./Protos/F1Grpc.proto
