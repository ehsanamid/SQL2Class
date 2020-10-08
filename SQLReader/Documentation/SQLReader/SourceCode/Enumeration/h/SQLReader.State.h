public enum class State sealed : System::Int32 {
    
    // Hex value is: 00000000
    [Description(L"Database is available for access. The primary filegroup is online, although the u" 
L"ndo phase of recovery may not have been completed.")]
    ONLINE = 0,
    
    // Hex value is: 00000001
    [Description(L"One or more files of the primary filegroup are being restored, or one or more sec" 
L"ondary files are being restored offline. The database is unavailable.")]
    RESTORING = 1,
    
    // Hex value is: 00000002
    [Description(L"Database is being recovered. The recovering process is a transient state; the dat" 
L"abase will automatically become online if the recovery succeeds. If the recovery" 
L" fails, the database will become suspect. The database is unavailable.")]
    RECOVERING = 2,
    
    // Hex value is: 00000003
    [Description(L"SQL Server has encountered a resource-related error during recovery. The database" 
L" is not damaged, but files may be missing or system resource limitations may be " 
L"preventing it from starting. The database is unavailable. Additional action by t" 
L"he user is required to resolve the error and let the recovery process be complet" 
L"ed.")]
    RECOVERY_PENDING = 3,
    
    // Hex value is: 00000004
    [Description(L"At least the primary filegroup is suspect and may be damaged. The database cannot" 
L" be recovered during startup of SQL Server. The database is unavailable. Additio" 
L"nal action by the user is required to resolve the problem.")]
    SUSPECT = 4,
    
    // Hex value is: 00000005
    [Description(L"User has changed the database and set the status to EMERGENCY. The database is in" 
L" single-user mode and may be repaired or restored. The database is marked READ_O" 
L"NLY, logging is disabled, and access is limited to members of the sysadmin fixed" 
L" server role. EMERGENCY is primarily used for troubleshooting purposes. For exam" 
L"ple, a database marked as suspect can be set to the EMERGENCY state. This could " 
L"permit the system administrator read-only access to the database. Only members o" 
L"f the sysadmin fixed server role can set a database to the EMERGENCY state.")]
    EMERGENCY = 5,
    
    // Hex value is: 00000006
    [Description(L"Database is unavailable. A database becomes offline by explicit user action and r" 
L"emains offline until additional user action is taken. For example, the database " 
L"may be taken offline in order to move a file to a new disk. The database is then" 
L" brought back online after the move has been completed.")]
    OFFLINE = 6,
};
