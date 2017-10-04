FROM microsoft/aspnetcore-build:1.1.2
ARG source
WORKDIR /app
RUN apt-get -qq update && apt-get -qqy --no-install-recommends install wget gnupg \
    git \
    unzip
RUN curl -sL https://deb.nodesource.com/setup_6.x |  bash -
RUN apt-get install -y nodejs
RUN npm install 
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
#ENTRYPOINT ["dotnet", "Saleman.Web.Api.dll"]
CMD ["dotnet", "Saleman.Web.Api.dll"]
