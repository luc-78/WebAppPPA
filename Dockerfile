FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build

LABEL maintainer="Luca Brighi <lucabrighi@msn.com>"
LABEL description="Docker file WebAppPPA"

WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
EXPOSE 5001
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/WebAppPPA.dll" ]



#Build ed Avvio contenitore docker
# dotnet publish -c Release
# docker build -t saluti -f Dockerfile .
# docker run --restart unless-stopped --log-opt max-size=10m -p 5051:80 saluti


#Push immagine in Docker Hub
#docker login
#docker tag saluti nlarocca/salutiwebapi:V001
#docker push nlarocca/salutiwebapi:V001

#Push immagine in Azure Container Registry
#docker login [server] --> inserire userid e passoword
#docker tag saluti [server]/salutiwebapi:[tag]
#docker push [server]/salutiwebapi:[tag]