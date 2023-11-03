FROM ubuntu/nginx:latest

RUN apt-get update
RUN apt-get install -y dotnet-sdk-7.0
RUN dotnet --version

EXPOSE 8000

WORKDIR /FIT_Project_Manager
ENTRYPOINT [ "dotnet", "run" ]