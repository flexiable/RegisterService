#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM swr.cn-south-1.myhuaweicloud.com/mcr/aspnet:3.1-alpine 
WORKDIR /app
COPY . . 
EXPOSE 8081 
ENTRYPOINT ["dotnet", "RegService.dll"]