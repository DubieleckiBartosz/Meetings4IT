SQLCMD="/opt/mssql-tools/bin/sqlcmd"
SERVER="localhost"
USERNAME="sa"
PASSWORD="sql123456(!)"

# Set the target database for create-db.sql
database_name="master"

# Execute create-db.sql first
if [ -f "create-db.sql" ]; then
    echo "Executing script: create-db.sql for database: $database_name"

    for attempt in {1..30}; do
        $SQLCMD -S $SERVER -U $USERNAME -P $PASSWORD -d $database_name -i "create-db.sql"
        if [ $? -eq 0 ]; then
            echo "Script create-db.sql executed successfully for database: $database_name"
            break
        else
            echo "Script create-db.sql not ready yet (Attempt $attempt) for database: $database_name..."
            sleep 2
        fi
    done
fi
 
for script_file in $(ls *.sql | sort); do
    if [ -f "$script_file" ] && [ "$script_file" != "create-db.sql" ]; then
        database_name="Meetings4IT"

        echo "Executing script: $script_file for database: $database_name"

        for attempt in {1..30}; do
            $SQLCMD -S $SERVER -U $USERNAME -P $PASSWORD -d $database_name -i "$script_file"
            if [ $? -eq 0 ]; then
                echo "Script $script_file executed successfully for database: $database_name"
                break
            else
                echo "Script $script_file not ready yet (Attempt $attempt) for database: $database_name..."
                sleep 2
            fi
        done
    fi
done