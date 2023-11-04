FROM postgres:latest

RUN mkdir scripts
# sourceはスクリプト名によって変更する必要あり
ADD ./create_tables.sql /scripts/

# CMD "psql -f /scripts/create_tables.sql" 