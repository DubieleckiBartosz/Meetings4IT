FROM mcr.microsoft.com/mssql/server:2019-latest 
USER root

ARG PROJECT_DIR=/tmp/mssql-scripts

RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
ENV SA_PASSWORD=sql123456(!)
ENV ACCEPT_EULA=Y
 
COPY ./database ./ 

COPY entrypoint.sh ./ 
COPY setupsql.sh ./ 

RUN chmod +x setupsql.sh
CMD ["/bin/bash", "entrypoint.sh"]