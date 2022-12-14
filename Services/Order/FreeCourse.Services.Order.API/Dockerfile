#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Order/FreeCourse.Services.Order.API/FreeCourse.Services.Order.API.csproj", "Services/Order/FreeCourse.Services.Order.API/"]
COPY ["Services/Order/FreeCourse.Services.Order.Application/FreeCourse.Services.Order.Application.csproj", "Services/Order/FreeCourse.Services.Order.Application/"]
COPY ["Services/Order/FreeCourse.Services.Order.Infrastructure/FreeCourse.Services.Order.Infrastructure.csproj", "Services/Order/FreeCourse.Services.Order.Infrastructure/"]
COPY ["Services/Order/FreeCourse.Services.Order.Domain/FreeCourse.Services.Order.Domain.csproj", "Services/Order/FreeCourse.Services.Order.Domain/"]
COPY ["Services/Order/FreeCourse.Services.Order.Domain.Core/FreeCourse.Services.Order.Domain.Core.csproj", "Services/Order/FreeCourse.Services.Order.Domain.Core/"]
COPY ["Shared/FreeCourse.Shared/FreeCourse.Shared.csproj", "Shared/FreeCourse.Shared/"]
COPY ["Services/Basket/FreeCourse.Services.Basket/FreeCourse.Services.Basket.csproj", "Services/Basket/FreeCourse.Services.Basket/"]
RUN dotnet restore "Services/Order/FreeCourse.Services.Order.API/FreeCourse.Services.Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/FreeCourse.Services.Order.API"
RUN dotnet build "FreeCourse.Services.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeCourse.Services.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeCourse.Services.Order.API.dll"]