FROM ubuntu/nginx:latest

RUN apt-get update
RUN apt-get install -y dotnet-sdk-7.0
RUN dotnet --version
# curlを入れないとdotnetのパッケージがインストールできない。多分curlを入れることでSSLへの接続ができるようになるためだと考える
RUN apt-get install -y curl

EXPOSE 8000

WORKDIR /FIT_Project_Manager

# COPY ./FIT_Project_Manager /FIT_Project_Manager/
# RUN dotnet restore --interactive
# RUN dotnet add package Ngpsql
# CMD [ "dotnet", "add", "package", "Npgsql" ]

# ENTRYPOINT [ "dotnet", "watch" ]
# ENTRYPOINT [ "dotnet", "add package Npgsql" ]

# CMD [ "dotnet", "run" ]