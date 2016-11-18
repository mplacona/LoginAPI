FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app
 
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
 
EXPOSE 5050/tcp
ENV ASPNETCORE_URLS http://*:5050
 
ENTRYPOINT ["dotnet", "bin/Release/netcoreapp1.0/publish/LoginAPI.dll"]