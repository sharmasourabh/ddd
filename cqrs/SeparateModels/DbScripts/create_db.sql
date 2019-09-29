-- cd DbScripts
-- "PostgreSQL\9.6\bin\psql.exe" --host "localhost" --port 5432 --username "postgres" --file "create_db.sql"

CREATE USER lab_user WITH ENCRYPTED PASSWORD 'lab_pass';

CREATE DATABASE lab_policy;
GRANT ALL PRIVILEGES ON DATABASE lab_policy TO lab_user;

CREATE DATABASE lab_cqrs_dotnet_demo
GRANT ALL PRIVILEGES ON DATABASE lab_cqrs_dotnet_demo TO lab_user;