## Postgresql Setup

1. Download the zip archive from postgresql download page
2. Unzip to c:\dev\postgresql
3. Execute following from pgsql\bin (One time activity to init/create DB)
   ```bash
   pgsql/bin> pg_ctl initdb --pgdata=c:\dev\postgresql\data
   ```
4. On success, `runpgsql/bin> pg_ctl -D c:/dev/postgresql/data -l logfile start`
5. On success. Start clieng pgAdmin from "pgAdmin 4\bin"
   OR
   Use any of the below clients (preferred over pgAdmin)
    - HeidiSQL
    - DBeaver