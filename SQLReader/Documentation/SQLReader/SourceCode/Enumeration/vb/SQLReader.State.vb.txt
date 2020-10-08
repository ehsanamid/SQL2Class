Public Enum State As Integer
    
    'Hex value is: 00000000
    <Description("Database is available for access. The primary filegroup is online, although the u"& _ 
        "ndo phase of recovery may not have been completed.")>  _
    ONLINE = 0
    
    'Hex value is: 00000001
    <Description("One or more files of the primary filegroup are being restored, or one or more sec"& _ 
        "ondary files are being restored offline. The database is unavailable.")>  _
    RESTORING = 1
    
    'Hex value is: 00000002
    <Description("Database is being recovered. The recovering process is a transient state; the dat"& _ 
        "abase will automatically become online if the recovery succeeds. If the recovery"& _ 
        " fails, the database will become suspect. The database is unavailable.")>  _
    RECOVERING = 2
    
    'Hex value is: 00000003
    <Description("SQL Server has encountered a resource-related error during recovery. The database"& _ 
        " is not damaged, but files may be missing or system resource limitations may be "& _ 
        "preventing it from starting. The database is unavailable. Additional action by t"& _ 
        "he user is required to resolve the error and let the recovery process be complet"& _ 
        "ed.")>  _
    RECOVERY_PENDING = 3
    
    'Hex value is: 00000004
    <Description("At least the primary filegroup is suspect and may be damaged. The database cannot"& _ 
        " be recovered during startup of SQL Server. The database is unavailable. Additio"& _ 
        "nal action by the user is required to resolve the problem.")>  _
    SUSPECT = 4
    
    'Hex value is: 00000005
    <Description("User has changed the database and set the status to EMERGENCY. The database is in"& _ 
        " single-user mode and may be repaired or restored. The database is marked READ_O"& _ 
        "NLY, logging is disabled, and access is limited to members of the sysadmin fixed"& _ 
        " server role. EMERGENCY is primarily used for troubleshooting purposes. For exam"& _ 
        "ple, a database marked as suspect can be set to the EMERGENCY state. This could "& _ 
        "permit the system administrator read-only access to the database. Only members o"& _ 
        "f the sysadmin fixed server role can set a database to the EMERGENCY state.")>  _
    EMERGENCY = 5
    
    'Hex value is: 00000006
    <Description("Database is unavailable. A database becomes offline by explicit user action and r"& _ 
        "emains offline until additional user action is taken. For example, the database "& _ 
        "may be taken offline in order to move a file to a new disk. The database is then"& _ 
        " brought back online after the move has been completed.")>  _
    OFFLINE = 6
End Enum
