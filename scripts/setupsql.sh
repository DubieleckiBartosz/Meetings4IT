SCRIPTS[0]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P sql123456(!) -d master -i panels-create-db.sql"  
SCRIPTS[1]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P sql123456(!) -d Meetings4IT -i panels-create-tables.sql" # -v VERBOSITY=1
SCRIPTS[2]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P sql123456(!) -d Meetings4IT -i panels-create-stored-procedures.sql"   


for ((i = 0; i < ${#SCRIPTS[@]}; i++))
do
    echo "Starting operation ${i}"
    for x in {1..30};
    do
        ${SCRIPTS[$i]}
        if [ $? -eq 0 ]
        then
            echo "Operation number ${i} completed"
            break
        else
            echo "Operation number ${i} not ready yet..."
            sleep 2
        fi
    done
done
