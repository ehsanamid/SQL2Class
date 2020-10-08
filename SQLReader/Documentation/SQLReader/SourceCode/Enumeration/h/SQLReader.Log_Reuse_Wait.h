public enum class Log_Reuse_Wait sealed : System::Byte {
    
    // Hex value is: 00000000
    [Description(L"Currently there are one or more reusable virtual log files.")]
    NOTHING = 0,
    
    // Hex value is: 00000001
    [Description(L"No checkpoint has occurred since the last log truncation, or the head of the log " 
L"has not yet moved beyond a virtual log file (all recovery models).This is a rout" 
L"ine reason for delaying log truncation. For more information, see Checkpoints an" 
L"d the Active Portion of the Log.")]
    CHECKPOINT = 1,
    
    // Hex value is: 00000002
    [Description(L"A log backup is required to move the head of the log forward (full or bulk-logged" 
L" recovery models only). Note:  Log backups do not prevent truncation. When the l" 
L"og backup is completed, the head of the log is moved forward, and some log space" 
L" might become reusable.")]
    LOG_BACKUP = 2,
    
    // Hex value is: 00000003
    [Description(L"A data backup or a restore is in progress (all recovery models). A data backup wo" 
L"rks like an active transaction and, when running, the backup prevents truncation" 
L". For more information, see \"Data Backup Operations and Restore Operations,\" lat" 
L"er in this topic.")]
    ACTIVE_BACKUP_OR_RESTORE = 3,
    
    // Hex value is: 00000004
    [Description(L"A transaction is active (all recovery models). A long-running transaction might e" 
L"xist at the start of the log backup. In this case, freeing the space might requi" 
L"re another log backup. For more information, see \"Long-Running Active Transactio" 
L"ns,\" later in this topic. A transaction is deferred (SQL Server 2005 Enterprise " 
L"Edition and later versions only). A deferred transaction is effectively an activ" 
L"e transaction whose rollback is blocked because of some unavailable resource. Fo" 
L"r information about the causes of deferred transactions and how to move them out" 
L" of the deferred state, see Deferred Transactions. ")]
    ACTIVE_TRANSACTION = 4,
    
    // Hex value is: 00000005
    [Description(L"Database mirroring is paused, or under high-performance mode, the mirror database" 
L" is significantly behind the principal database (full recovery model only). For " 
L"more information, see \"Database Mirroring and the Transaction Log,\" later in thi" 
L"s topic.")]
    DATABASE_MIRRORING = 5,
    
    // Hex value is: 00000006
    [Description(L"During transactional replications, transactions relevant to the publications are " 
L"still undelivered to the distribution database (full recovery model only). For m" 
L"ore information, see \"Transactional Replication and the Transaction Log,\" later " 
L"in this topic.")]
    REPLICATION = 6,
    
    // Hex value is: 00000007
    [Description(L"A database snapshot is being created (all recovery models). This is a routine, an" 
L"d typically brief, cause of delayed log truncation.")]
    DATABASE_SNAPSHOT_CREATION = 7,
    
    // Hex value is: 00000008
    [Description(L"A log scan is occurring (all recovery models). This is a routine, and typically b" 
L"rief, cause of delayed log truncation.")]
    LOG_SCAN = 8,
    
    // Hex value is: 00000009
    [Description(L"This value is currently not used.")]
    OTHER_TRANSIENT = 9,
};
